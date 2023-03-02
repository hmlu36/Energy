using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Energy.Services
{

    public interface IFlowService
    {

        public List<DropItem> GetFlowTree();
    }

    public class FlowService : GenericService, IFlowService
    {
        public FlowService(EnergyDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<DropItem> GetFlowTree()
        {
            return BuildTree(string.Empty, _mapper.Map<List<DropItem>>(_context.TFlows.ToList()));
        }

        private static List<DropItem> BuildTree(string parentId, List<DropItem> energies)
        {
            return energies.Where(e => e.ParentId == parentId)
                           .Select(e =>
                           {
                               e.Children = BuildTree(e.Id, energies);
                               return e;
                           }).OrderBy(e => e.Iorder)
                           .ToList();
        }
    }
}
