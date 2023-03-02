using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JamZoo.Project.WebSite.Attribute
{
    public class BeAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                
                string returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                RouteValueDictionary RVD = new RouteValueDictionary();
                RVD.Add("controller", "account");
                RVD.Add("action", "login");
                RVD.Add("returnurl", returnUrl);
                
                filterContext.Result = new RedirectToRouteResult(RVD);
            }
        }
    }
}

