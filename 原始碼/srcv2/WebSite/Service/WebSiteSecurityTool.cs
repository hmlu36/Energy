using JamZoo.Project.WebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.Service
{
    using JamZoo.Project.WebSite.Enums;
    using Library;

    public class WebSiteSecurityTool
    {
        public AccountService AccountSrv;

        public WebSiteSecurityTool(
            AccountService a)
        {
            AccountSrv = a;
        }
    }
}