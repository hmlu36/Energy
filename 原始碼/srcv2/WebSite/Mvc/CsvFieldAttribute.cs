using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.Mvc
{
    
    
    
    public class CsvFieldAttribute : System.Attribute
    {
        public string DisplayName { get; set; }

        public CsvFieldAttribute() { }

        public CsvFieldAttribute(string DisplayName)
        {
            this.DisplayName = DisplayName;
        }
    }
}
