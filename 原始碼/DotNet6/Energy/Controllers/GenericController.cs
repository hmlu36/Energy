
using Microsoft.AspNetCore.Mvc;

namespace Energy.Controllers
{
    public class GenericController : ControllerBase
    {
        protected readonly ILogger<GenericController> _logger;

        public GenericController(ILogger<GenericController> logger)
        {
            _logger = logger;
        }
    }
}
