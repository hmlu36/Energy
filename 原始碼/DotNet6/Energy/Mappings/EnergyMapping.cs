using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;
using Energy.Models.ViewModels.Flow;

namespace Energy.Mappings
{
    public class EnergyMapping : Profile
    {

        public EnergyMapping()
        {
            CreateMap<TFlow, DropItem>();
            //CreateMap<TFlow, EnergyModel>();
        }
    }
}
