using AutoMapper;
using Energy.Models.Constants;
using Energy.Models.DB;
using Energy.Models.Enums;
using Energy.Models.ViewModels.Database;
using Energy.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public DatabaseService(
            EnergyDbContext context,
            IMapper mapper,
            ILogger<GenericService> logger,
            IEnergyService energyService,
            IFlowService flowService) : base(context, mapper, logger)
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
                e.LastUpdate = DateUtil.ToAD(_context.TSystems.FirstOrDefault(e => e.Mk == SystemConstant.DatabaseLastUpdateTime).Mv);
                e.DdlLastDate = DateUtil.ToAD(_context.TSystems.FirstOrDefault(e => e.Mk == SystemConstant.DatabaseDdlLastDay).Mv);
            });

            return data;
        }


        public List<DatabaseQueryResult> Query(DatabaseCriteria criteria)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(criteria));
            List<DatabaseQueryResult> result = new List<DatabaseQueryResult>();
            var dbEnergies = _context.TEnergies.Where(e => criteria.EnergySelectedValue.Contains(e.Id));
            var dbFlows = _context.TFlows.Where(e => criteria.FlowSelectedValue.Contains(e.Id));

            foreach (var energy in dbEnergies)
            {
                var sql =
                    $@"select * from {energy.TableName}
                where exists (
                    select 1 from T_Flow
                    where id in ('{string.Join("','", dbFlows.Select(f => f.RowNo1))}')
                    and T_Flow.row_no1 = {energy.TableName}.row_no1
                )";

                var entity = ObjectUtil.CreateInstance(energy.TableName);
                _logger.LogInformation(sql);
                result.Add(new DatabaseQueryResult()
                {
                    Data = _context.Set<object>().FromSqlRaw(sql),
                    Title = energy.Name
                });
            }

            return result;
        }
    }
}
