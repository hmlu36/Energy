using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Models;

namespace JamZoo.Project.WebSite.ViewModels
{
    
    
    
    public class IndexPage
    {
        public PublicationListModel PubData { get; set; } 

        public ThematicListModel TheData { get; set; }

        public BannerListModel BanData { get; set; }
    }

}
