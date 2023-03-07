using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;

namespace Energy.Mappings
{
    public class FlowMapping : Profile
    {

        public FlowMapping()
        {
            CreateMap<TFlow, DropItem>();
        }
    }
}
