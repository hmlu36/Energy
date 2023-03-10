using AutoMapper;
using Dapper;
using Energy.Models.Constants;
using Energy.Models.DB;
using Energy.Models.Enums;
using Energy.Models.ViewModels.Database;
using Energy.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Energy.Services
{
    public interface IDatabaseService
    {
        public List<DatabaseType> GetSearchPageSetting();

        public List<DatabaseQueryResult> Query(DatabaseCriteria criteria);
    }
    public class DatabaseService : GenericService, IDatabaseService
    {
        private readonly IEnergyService _energyService;
        private readonly IFlowService _flowService;
        private readonly string newline = Environment.NewLine;

        public DatabaseService(
            EnergyDbContext context,
            DapperContext dapperContext,
            IMapper mapper,
            ILogger<GenericService> logger,
            IEnergyService energyService,
            IFlowService flowService
        ) : base(context, dapperContext, mapper, logger)
        {
            _energyService = energyService;
            _flowService = flowService;
        }

        public List<DatabaseType> GetSearchPageSetting()
        {
            var data = new List<DatabaseType>() {
                new DatabaseType {
                    Id = 0, Name = "能源供需", DisplayName = "能源供需-OECD格式", Left = "能源別", Right = "流程別",
                    UnitList = new List<string> { "原始單位", "熱值單位", "油當量單位" },
                },
                new DatabaseType {
                    Id = 1, Name = "能源進口來源", DisplayName = "能源進口來源", Left = "能源別", Right = "",
                    UnitList = new List<string> { "原始單位" },
                },
                new DatabaseType {
                    Id = 2, Name = "常用能源指標", DisplayName = "常用能源指標", Left = "項目", Right = "",
                    UnitList = new List<string> { "原始單位" },
                },
                new DatabaseType {
                    Id = 3, Name = "發電量", DisplayName = "發電量", Left = "類別", Right = "能源別",
                    UnitList = new List<string> { "度" },
                },
                new DatabaseType {
                    Id = 4, Name = "發電裝置容量", DisplayName = "發電裝置容量", Left = "類別", Right = "能源別",
                    UnitList = new List<string> { "千瓩" },
                },
                new DatabaseType {
                    Id = 5, Name = "發電燃料投入", DisplayName = "發電燃料投入", Left = "能源別", Right = "類別",
                    UnitList = { "原始單位", "熱值單位", "油當量單位" },
                },
            };

            var allEnergyItems = _energyService.GetEnergyTree();
            var allFlowItems = _flowService.GetFlowTree();
            data.ForEach(e =>
            {
                e.EnergyDropDownList = allEnergyItems.Where(a => a.PageName == e.Name);
                e.FlowDropDownList = allFlowItems.Where(a => a.PageName == e.Name);
                e.YearTypes = Enum.GetValues(typeof(YearType)).Cast<YearType>();
                e.PeriodTypes = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>();
                e.StartDate = new DateTime(1982, 1, 1);
                e.LastUpdate = DateUtil.ToAD(_context.TSystems.FirstOrDefault(e => e.Mk == SystemConstant.DatabaseLastUpdateTime)?.Mv ?? string.Empty);
                e.DdlLastDate = DateUtil.ToAD(_context.TSystems.FirstOrDefault(e => e.Mk == SystemConstant.DatabaseDdlLastDay)?.Mv ?? string.Empty);
            });

            return data;
        }

        public List<DatabaseQueryResult> Query(DatabaseCriteria criteria)
        {
            _logger.LogDebug($"database criteria: {JsonConvert.SerializeObject(criteria)}");

            // 初始預設值
            var now = DateTime.Now;
            var yearOffset = 0;

            // 西元/民國
            // 西元年減去1911
            if (YearType.ROC == (criteria.YearType ?? YearType.ROC))
            {
                yearOffset = 1911;
            }

            // 起始日
            if (string.IsNullOrEmpty(criteria.StartDate))
            {
                criteria.StartDate = $"{(now.Year - yearOffset - 2)}01";
            }

            // 結束日
            if (string.IsNullOrEmpty(criteria.EndDate))
            {
                criteria.EndDate = $"{(now.Year - yearOffset)}{now.Month.ToString("D2")}";
            }

            // 單位
            if (!criteria.UnitType.HasValue)
            {
                criteria.UnitType = 0;
            }

            var dbEnergies = _context.TEnergies.Where(e => criteria.EnergySelectedValue.Contains(e.Id)).OrderBy(e => e.Iorder);

            List<DatabaseQueryResult> entries = new List<DatabaseQueryResult>();

            var Title = string.Empty;
            foreach (var energy in dbEnergies)
            {
                entries.Add(GetByEnergyIssue(criteria, energy));

            }
            return entries;
        }


        private DatabaseQueryResult GetByEnergyIssue(DatabaseCriteria criteria, TEnergy dbEnergy)
        {
            var energyIssue = (EnergyIssue)Enum.Parse(typeof(EnergyIssue), dbEnergy.Id.Substring(0, 1));
            switch (energyIssue)
            {
                // 能源供需
                case EnergyIssue.EnergySupplyDemand:
                // 發電量
                case EnergyIssue.PowerGeneration:
                // 發電裝置容量
                case EnergyIssue.PowerCapacity:
                // 發電燃料投入
                case EnergyIssue.PowerFuelInput:
                    return GetByEnergyAndFlowSetting(criteria, dbEnergy, energyIssue);

                // 能源進口來源
                case EnergyIssue.EnergyImportSource:
                // 常用能源指標
                case EnergyIssue.CommonEnergyIndicators:
                    return GetByEnergySetting(criteria, dbEnergy, energyIssue);

                default:
                    return new DatabaseQueryResult();
            }
        }

        // 從T_Energy及T_Flow抓取資料來源設定
        // TEnergy.TableName        : 資料來源
        // TEnergy.UnitListBotton   : 單位(陣列)
        // TEnergy.ColIdList        : 對應欄位(陣列)
        // T_Flow.Row_No1           : 資料來源對應代碼
        private DatabaseQueryResult GetByEnergyAndFlowSetting(DatabaseCriteria criteria, TEnergy dbEnergy, EnergyIssue energyIssue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("startDate", criteria.StartDate);
            parameters.Add("endDate", criteria.EndDate);

            var dbFlows = _context.TFlows.Where(e => criteria.FlowSelectedValue.Contains(e.Id)).ToList();

            var sql = string.Empty;
            foreach (var dbFlow in dbFlows)
            {
                var randomValue = RandomUtil.GetRandomValue();
                sql += (string.IsNullOrEmpty(sql) ? "" : newline + " union ") + newline +
                $@"select '{dbFlow.Id}' id              
                        , '{dbFlow.Name}' name           
                        , yr_mnth                        
                        , round(sum({GetSummationColumn(energyIssue, dbEnergy, dbFlow, criteria.UnitType ?? 0)}), {dbFlow.DecimalPlaces}) data  
                     from {GetEnergyDataSource(energyIssue)}
                    where row_no1 in @rowNo1s{randomValue}  
                      and yr_mnth >= @startDate             
                      and yr_mnth <= @endDate               
                    group by yr_mnth";

                parameters.Add($"rowNo1s{randomValue}",
                                (string.IsNullOrEmpty(dbEnergy.RowNo1) ? dbFlow.RowNo1 : dbEnergy.RowNo1)
                                .Split(',')
                                .DefaultIfEmpty()
                                .Distinct());
            }

            string sqlPeriodSummationColumn = GetPeriodSummationColumn(criteria);
            sql = newline +
                  $@"select 
                            id, 
                            name,
                            {sqlPeriodSummationColumn} period, 
                            sum(data) data
                       from ( 
                        {sql}
                       ) temp 
                     group by id, name, {sqlPeriodSummationColumn}
                     order by id";
            using (var conn = _dapperContext.CreateConnection())
            {
                _logger.LogDebug($"sql:{sql}");

                foreach (string name in parameters.ParameterNames)
                {
                    _logger.LogDebug(name + ":" + JsonConvert.SerializeObject(parameters.Get<object>(name)));
                }

                var flowEntry = conn.Query(sql, parameters);

                return new DatabaseQueryResult()
                {
                    Title = dbEnergy.Name ?? string.Empty,
                    Header = flowEntry.Where(e => e.name == dbFlows?.FirstOrDefault()?.Name)
                                        .Select(e => e.period)
                                        .Prepend("日期"),
                    Content = flowEntry.GroupBy(e => e.name)
                                         .Select(group => new object[] { group.Key }.Concat(group.Select(g => g.data)).ToArray())
                                         .ToArray()
                };
            }
        }

        private string GetPeriodSummationColumn(DatabaseCriteria criteria)
        {
            _logger.LogDebug($"YearType:{criteria.YearType}");
            // 西元年須加上1911
            var yearColumn = $"CAST((yr_mnth / 100) + {(YearType.ROC == (criteria.YearType ?? YearType.ROC) ? 0 : 1911) } as varchar) + '年'";
            switch (criteria.PeriodType)
            {
                case null:
                case PeriodType.M:
                    return $"{yearColumn} + FORMAT(yr_mnth%100, '00') + '月'";
                case PeriodType.Q:
                    return $"{yearColumn} + '第' + TRANSLATE(CAST(CEILING(CAST(RIGHT(yr_mnth, 2) AS int) / 3.0) as varchar), '1234', '一二三四') + '季'";
                case PeriodType.Y:
                    return yearColumn;
                default:
                    return $"{yearColumn} + FORMAT(yr_mnth%100, '00') + '月'";
            }
        }

        // 取得Energy對應來源資料表, 因為資料庫與實際資料不符
        private string GetEnergyDataSource(EnergyIssue energyIssue)
        {
            switch (energyIssue)
            {
                // 能源供需
                case EnergyIssue.EnergySupplyDemand:
                    return "wesdes50_db";
                // 發電量
                case EnergyIssue.PowerGeneration:
                    return "power50_db";
                // 發電裝置容量
                case EnergyIssue.PowerCapacity:
                    return "power50";
                // 發電燃料投入
                case EnergyIssue.PowerFuelInput:
                    return "fuel50_db";
                // 能源進口來源
                case EnergyIssue.EnergyImportSource:
                    return "coal50";
                // 常用能源指標
                case EnergyIssue.CommonEnergyIndicators:
                    return "energy50_db";
                default:
                    return string.Empty;
            }
        }

        // 根據資料庫table取得對應class名稱
        private string GetEnergyDataSourceClassName(EnergyIssue energyIssue)
        {
            var energyDataSource = GetEnergyDataSource(energyIssue);

            return string.Concat(
                        energyDataSource.Split('_').Select((s, i) => i == 0 ?
                                                                     s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower() :
                                                                     s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower())
                    );
        }

        // 取得查詢欄位(可能多個加總)
        private string GetSummationColumn(EnergyIssue energyIssue, TEnergy dbEnergy, TFlow dbFlow, int unitType)
        {
            var energyColumn = dbEnergy.ColIdList?.Split(',')[unitType] ?? string.Empty;
            var flowColumns = dbFlow.ColIdList?.TrimEnd(new char[] { ',' }).Split(',');
            var summationColumns = new List<string>();

            switch (energyIssue)
            {
                // 能源供需
                case EnergyIssue.EnergySupplyDemand:
                    return energyColumn;
                // 發電量
                case EnergyIssue.PowerGeneration:
                // 發電裝置容量
                case EnergyIssue.PowerCapacity:
                    summationColumns = flowColumns?.Select(c => $"{energyColumn}{c}").ToList();
                    break;
                // 發電燃料投入
                case EnergyIssue.PowerFuelInput:
                    summationColumns = flowColumns?.Select(c => $"{c}{energyColumn}").ToList();
                    break;
                default:
                    return string.Empty;
            }

            _logger.LogDebug($"summationColumns:{JsonConvert.SerializeObject(summationColumns)}");
            var energyDataSource = GetEnergyDataSourceClassName(energyIssue);
            _logger.LogDebug($"energyDataSource:{energyDataSource}");
            var memeberNames = ObjectUtil.GetClassDbMemeberNames($"Energy.Models.DB.{energyDataSource}");
            _logger.LogDebug($"memeberNames:{JsonConvert.SerializeObject(memeberNames)}");

            if (summationColumns == null || summationColumns.Count() == 0)
            {
                return string.Empty;
            }
            // 只有一個欄位時, 判斷是否存在, 不存在回傳NULLIF(0, 0), 加總完為null
            else if (summationColumns?.Count() == 1)
            {
                return summationColumns?.FirstOrDefault(c => memeberNames.Contains(c)) ?? "NULLIF(0, 0)";
            }
            else
            {
                return string.Join(" + ", summationColumns?.Intersect(memeberNames));
            }
        }

        private DatabaseQueryResult GetByEnergySetting(DatabaseCriteria criteria, TEnergy dbEnergy, EnergyIssue energyIssue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("startDate", criteria.StartDate);
            parameters.Add("endDate", criteria.EndDate);
            var sql = string.Empty;
            var codes = GetCodes(dbEnergy, energyIssue);

            _logger.LogInformation($"codes:{JsonConvert.SerializeObject(codes)}");
            foreach (var code in codes)
            {
                var randomValue = RandomUtil.GetRandomValue();
                sql +=
                $@"{(string.IsNullOrEmpty(sql) ? "" : newline + " union ")}
                    select '{dbEnergy.Id}' id   
                        , '{GetColumnMapping(energyIssue, dbEnergy, code)}' name
                        , yr_mnth                  
                        , sum({code}) data                   
                     from {GetEnergyDataSource(energyIssue)} 
                    where row_no1 in @rowNo1s{randomValue}   
                      and yr_mnth >= @startDate              
                      and yr_mnth <= @endDate                
                    group by yr_mnth";

                parameters.Add($"rowNo1s{randomValue}", dbEnergy.RowNo1?.Split(',')
                                                                       .DefaultIfEmpty()
                                                                       .Distinct());
            }

            string sqlPeriodSummationColumn = GetPeriodSummationColumn(criteria);
            sql = newline +
                  $@"select 
                            id, 
                            name,
                            {sqlPeriodSummationColumn} period, 
                            sum(data) data
                       from ( 
                        {sql}
                       ) temp 
                     group by id, name, {sqlPeriodSummationColumn}
                     order by id";
            _logger.LogDebug(sql);
            foreach (string name in parameters.ParameterNames)
            {
                _logger.LogDebug(name + ":" + JsonConvert.SerializeObject(parameters.Get<object>(name)));
            }
            using (var conn = _dapperContext.CreateConnection())
            {
                var energyEntry = conn.Query(sql, parameters);

                _logger.LogDebug($"energyEntry:{JsonConvert.SerializeObject(energyEntry)}");
                return new DatabaseQueryResult()
                {
                    Title = dbEnergy.Name ?? string.Empty,
                    Header = energyEntry.Where(e => e.name == GetColumnMapping(energyIssue, dbEnergy, codes.FirstOrDefault() ?? string.Empty))
                                        .Select(e => e.period)
                                        .Prepend("日期"),
                    Content = energyEntry.GroupBy(e => e.name)
                                        .Select(group => new object[] { group.Key }.Concat(group.Select(g => g.data)).ToArray())
                                        .ToArray()
                };
            }
        }

        private List<string> GetCodes(TEnergy dbEnergy, EnergyIssue energyIssue)
        {
            var codes = dbEnergy.ColIdList?.TrimEnd(new char[] { ',' }).Split(GetDelimiter(energyIssue)).Where(s => !string.IsNullOrEmpty(s.Trim())).ToList();
            if (codes == null) return new List<string>();

            switch (energyIssue)
            {
                // 常用能源指標(只取第一個欄位)
                case EnergyIssue.CommonEnergyIndicators:
                    return new List<string> { codes.FirstOrDefault() ?? string.Empty };
                default:
                    return codes;
            }
        }

        private Char GetDelimiter(EnergyIssue energyIssue)
        {
            switch (energyIssue)
            {
                case EnergyIssue.EnergyImportSource:
                    return ',';
                // 常用能源指標
                case EnergyIssue.CommonEnergyIndicators:
                    return '@';
                default:
                    return default(Char);
            }
        }

        private string GetColumnMapping(EnergyIssue energyIssue, TEnergy dbEnergy, string code)
        {
            switch (energyIssue)
            {
                // 能源進口來源
                case EnergyIssue.EnergyImportSource:
                    return CountryCode.CountryCodeNameMapping[code];
                // 常用能源指標
                case EnergyIssue.CommonEnergyIndicators:
                    return dbEnergy.Name ?? string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}
