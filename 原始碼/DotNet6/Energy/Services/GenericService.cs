using AutoMapper;
using Energy.Models.DB;
using ILogger = Serilog.Log;

namespace Energy.Services
{
    public class GenericService
    {
        protected readonly EnergyDbContext _context;

        protected readonly IMapper _mapper;

        protected readonly ILogger<GenericService> _logger;

        public GenericService(EnergyDbContext context, IMapper mapper, ILogger<GenericService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
