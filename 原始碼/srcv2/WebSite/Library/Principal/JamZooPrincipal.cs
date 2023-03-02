using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace JamZoo.Project.WebSite.Library.Principal
{
    
    
    
    public class WebSiteUser
    {
        
        
        
        public int MyMemberId { get; set; }
        public string Id { get; set; }
        public string AccountType { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }

    
    
    
    public class JamZooPrincipal : IPrincipal
    {
        public IIdentity Identity
        {
            get;
            private set;
        }

        public JamZooPrincipal(IIdentity identity)
        {

            Identity = identity;
        }

        public WebSiteUser User { get; set; }

        public bool IsInRole(string role)
        {
            if (User != null)
            {
                if (User.Roles != null)
                {
                    if (User.Roles.Length > 0)
                    {
                        return User.Roles.Contains(role);
                    }
                }
            }
            return false;
        }
    }
}
