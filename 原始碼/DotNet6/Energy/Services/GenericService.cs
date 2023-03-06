using AutoMapper;
using Energy.Models.DB;
using Energy.Utils;
using ILogger = Serilog.Log;

namespace Energy.Services
{
    public class GenericService
    {
        protected readonly EnergyDbContext _context;

        protected readonly DapperContext _dapperContext;

        protected readonly IMapper _mapper;

        protected readonly ILogger<GenericService> _logger;

        public GenericService(
            EnergyDbContext context,
            DapperContext dapperContext,
            IMapper mapper,
            ILogger<GenericService> logger
        )
        {
            _context = context;
            _dapperContext = dapperContext;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
