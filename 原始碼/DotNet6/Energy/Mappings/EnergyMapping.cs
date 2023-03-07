using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels.Database;

namespace Energy.Mappings
{
    public class EnergyMapping : Profile
    {

        public EnergyMapping()
        {
            CreateMap<TEnergy, DropItem>();
        }
    }
}
