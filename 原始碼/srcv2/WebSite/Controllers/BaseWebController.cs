using JamZoo.Project.WebSite.Attribute;
using System.Web.Mvc;

namespace JamZoo.Project.WebSite.Controllers
{
    using Service;
    public class CustomAttribute1 : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SystemService SystemSrv = new SystemService();
            if (filterContext.HttpContext.Session["webCount"] == null)
            {
                filterContext.HttpContext.Session["webCount"] = "ok";
                SystemSrv.瀏覽人次加一();
            }

            string temp = SystemSrv.GetValueByKey("瀏覽人次");
            filterContext.Controller.ViewData["瀏覽人次"] = temp;
        }

    }

    [CustomAttribute1]
    public class BaseWebController : Controller
    {
    }
}

