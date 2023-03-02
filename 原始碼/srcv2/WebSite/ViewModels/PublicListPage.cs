using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.ViewModels
{
    using Models;
    public class PublicListPage
    {
        public string LastUpdate { get; set; }
        public PublicParentListModel Data { get; set; }
        public string Id { get; set; }
        public PublicListPage()
        {
            Id = "";
        }

    }

}