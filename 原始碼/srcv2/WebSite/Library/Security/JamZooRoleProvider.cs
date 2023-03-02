using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers;
using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.Service;

namespace JamZoo.Project.WebSite.Library.Security
{
    public class BasicRoleProvider : DefaultRoleProvider
    {
        
        public override string[] GetRolesForUser(string userid)
        {
            AccountService service = new AccountService();
            AccountModel m = service.Get("role provider", userid);

            if (m != null)
            {
                return m.RoleList.Select(p => p.ToString()).ToArray<string>();
            }

            return null;
        }
    }
}
