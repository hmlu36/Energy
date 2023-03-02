using AutoMapper;
using Energy.Models.DB;
using Energy.Models.ViewModels;

namespace Energy.Mappings
{
    public class DatabaseMapping : Profile
    {
        public DatabaseMapping()
        {
            CreateMap<TEnergy, DropItem>();
            CreateMap<TFlow, DropItem>();
        }
    }
}
