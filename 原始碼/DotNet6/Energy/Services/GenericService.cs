using AutoMapper;
using Energy.Models.DB;

namespace Energy.Services
{
    public class GenericService
    {
        protected readonly EnergyDbContext _context;

        protected readonly IMapper _mapper;

        public GenericService(EnergyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
