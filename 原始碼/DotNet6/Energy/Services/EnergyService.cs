using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Energy.Services
{
    public interface IEnergyService
    {
        public string GetName(string id);

        public List<DropItem> GetEnergyTree();
    }

    public class EnergyService : GenericService, IEnergyService
    {
        public EnergyService(EnergyDbContext context, IMapper mapper) : base(context, mapper)
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

        public string GetName(string id)
        {
            return $"{id}:Bill";
        }
    }
}
