using Energy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Energy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyService _energyService;
        public EnergyController(IEnergyService energyService)
        {
            _energyService = energyService;
        }
    }
}
