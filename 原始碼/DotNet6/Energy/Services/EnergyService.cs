using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;
using Energy.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Energy.Services
{
    public interface IEnergyService
    {

        public List<DropItem> GetEnergyTree();

        /// <summary>
        /// 取得能源資料
        /// </summary>
        /// <param name="energySelectedValues"></param>
        /// <returns></returns>
        public IEnumerable<TEnergy> GetList(List<string> energySelectedValues);
    }

    public class EnergyService : GenericService, IEnergyService
    {
        public EnergyService(
            EnergyDbContext context,
            DapperContext dapperContext, 
            IMapper mapper, 
            ILogger<GenericService> logger
        ) : base(context, dapperContext, mapper, logger)
        {
        }

        public List<DropItem> GetEnergyTree()
        {
            return BuildTree(string.Empty, _mapper.Map<List<DropItem>>(_context.TEnergies.ToList()));
        }

        private static List<DropItem> BuildTree(string parentId, List<DropItem> energies)
        {
            return energies.Where(e => e.ParentId == parentId)
                           .Select(e =>
                           {
                               e.Children = BuildTree(e.Id, energies);
                               e.ShowCheckBox = !(e.PageName == "常用能源指標" && e.Depth == 0);
                               return e;
                           }).OrderBy(e => e.Iorder)
                           .ToList();
        }

        public IEnumerable<TEnergy> GetList(List<string> energySelectedValues)
        {
            throw new NotImplementedException();
        }
    }
}
