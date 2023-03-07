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

            var yearType = YearType.AD;
            var yearOffset = 0;

            // 西元/民國
            if (!criteria.YearType.HasValue)
            {
                criteria.YearType = YearType.AD;
            }

            // 西元年減去1911
            if (YearType.AD == yearType)
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

            var dbFlows = _context.TFlows.Where(e => criteria.FlowSelectedValue.Contains(e.Id));

            var sql = string.Empty;
            foreach (var dbFlow in dbFlows)
            {
                var randomValue = RandomUtil.GetRandomValue();
                sql += (string.IsNullOrEmpty(sql) ? "" : newline + " union ") + newline +
                $"select '{dbFlow.Name}' name                  " + newline +
                $"     , yr_mnth yearMonth                     " + newline +
                $"     , sum({GetSummationColumn(energyIssue, dbEnergy, dbFlow, criteria.UnitType ?? 0)}) data " + newline +
                $"  from {dbEnergy.TableName}                  " + newline +
                $" where row_no1 in @rowNo1s{randomValue}      " + newline +
                $"   and yr_mnth >= @startDate                 " + newline +
                $"   and yr_mnth <= @endDate                   " + newline +
                $" group by yr_mnth";

                parameters.Add($"rowNo1s{randomValue}", dbFlow.RowNo1.Split(',')
                                                                   .DefaultIfEmpty()
                                                                   .Distinct());
            }

            using (var conn = _dapperContext.CreateConnection())
            {
                _logger.LogDebug(sql);

                foreach (string name in parameters.ParameterNames)
                {
                    _logger.LogDebug(name + ":" + JsonConvert.SerializeObject(parameters.Get<object>(name)));
                }

                var flowEntry = conn.Query(sql, parameters);

                return new DatabaseQueryResult()
                {
                    Title = dbEnergy.Name ?? string.Empty,
                    Header = flowEntry.Where(e => e.name == dbFlows?.FirstOrDefault()?.Name)
                                        .Select(e => e.yearMonth.ToString().Substring(0, e.yearMonth.ToString().Length - 2) + "年" +
                                                     e.yearMonth.ToString().Substring(e.yearMonth.ToString().Length - 2))
                                        .Prepend("日期"),
                    Content = flowEntry.GroupBy(e => e.name)
                                         .Select(group => new object[] { group.Key }.Concat(group.Select(g => g.data)).ToArray())
                                         .ToArray()
                };
            }
        }

        private string GetSummationColumn(EnergyIssue energyIssue, TEnergy dbEnergy, TFlow dbFlow, int unitType)
        {
            switch (energyIssue)
            {
                // 能源供需
                case EnergyIssue.EnergySupplyDemand:
                    return dbEnergy.ColIdList?.Split(',')[unitType] ?? string.Empty;
                // 發電量
                case EnergyIssue.PowerGeneration:
                    var energyColumn = dbEnergy.ColIdList?[0];
                    var flowColumns = dbFlow.ColIdList?.TrimEnd(new char[] { ',' }).Split(',');
                    var summationColumns = flowColumns?.Select(c => $"{energyColumn}{c}");
                    var power50DbColumns = new Power50Db().GetType().GetProperties().Select(p => p.Name.ToLower());
                    _logger.LogDebug("Power50Dbs Columns:" + JsonConvert.SerializeObject(power50DbColumns));
                    _logger.LogDebug("summationColumn:" + JsonConvert.SerializeObject(summationColumns?.Where(c => power50DbColumns.Contains(c))));
                    // 只有一個欄位時, 判斷是否存在, 不存在回傳NULLIF(0, 0), 加總完為null
                    if (summationColumns?.Count() == 1) { 
                        return power50DbColumns.Contains(summationColumns.FirstOrDefault()) ? summationColumns?.FirstOrDefault() ?? string.Empty : "NULLIF(0, 0)";
                    } else {
                        return string.Join(" + ", summationColumns.Where(c => power50DbColumns.Contains(c)));
                    }
                default:
                    return string.Empty;
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
                sql += (string.IsNullOrEmpty(sql) ? "" : newline + " union ") + newline +
                $"select '{GetColumnMapping(energyIssue, dbEnergy, code)}' name" + newline +
                $"     , yr_mnth yearMonth               " + newline +
                $"     , sum({code}) data         " + newline +
                $"  from {dbEnergy.TableName}            " + newline +
                $" where row_no1 in @rowNo1s{randomValue}" + newline +
                $"   and yr_mnth >= @startDate           " + newline +
                $"   and yr_mnth <= @endDate             " + newline +
                $" group by yr_mnth";

                parameters.Add($"rowNo1s{randomValue}", dbEnergy.RowNo1?.Split(',')
                                                                       .DefaultIfEmpty()
                                                                       .Distinct());
            }

            _logger.LogDebug(sql);
            foreach (string name in parameters.ParameterNames)
            {
                _logger.LogDebug(name + ":" + JsonConvert.SerializeObject(parameters.Get<object>(name)));
            }
            using (var conn = _dapperContext.CreateConnection())
            {
                var energyEntry = conn.Query(sql, parameters);
                return new DatabaseQueryResult()
                {
                    Title = dbEnergy.Name ?? string.Empty,
                    Header = energyEntry.Where(e => e.name == GetColumnMapping(energyIssue, dbEnergy, codes.FirstOrDefault() ?? string.Empty))
                                        .Select(e => e.yearMonth.ToString().Substring(0, e.yearMonth.ToString().Length - 2) + "年" +
                                                     e.yearMonth.ToString().Substring(e.yearMonth.ToString().Length - 2))
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
