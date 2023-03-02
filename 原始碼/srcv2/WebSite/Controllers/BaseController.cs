using JamZoo.Project.WebSite.Attribute;
using System.Web.Mvc;

namespace JamZoo.Project.WebSite.Controllers
{
   
    [BeAuthorize(Roles="sa")]
    public class BaseController : Controller
    {
    }
}

