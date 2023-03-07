using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;
using Energy.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Energy.Services
{

    public interface IFlowService
    {
        /// <summary>
        /// 建置Flow
        /// </summary>
        /// <returns></returns>
        public List<DropItem> GetFlowTree();
    }

    public class FlowService : GenericService, IFlowService
    {
        public FlowService(
            EnergyDbContext context,
            DapperContext dapperContext,
            IMapper mapper,
            ILogger<GenericService> logger
        ) : base(context, dapperContext, mapper, logger)
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
