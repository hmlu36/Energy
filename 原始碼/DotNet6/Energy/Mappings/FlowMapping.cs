using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;
using Energy.Models.ViewModels.Flow;

namespace Energy.Mappings
{
    public class FlowMapping : Profile
    {

        public FlowMapping()
        {
            CreateMap<TFlow, DropItem>();
            CreateMap<TFlow, FlowModel>();
        }
    }
}
