/*
ModelName
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.Service;
using JamZoo.Project.WebSite.ViewModels;
using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Library.Principal;

namespace JamZoo.Project.WebSite.Controllers
{
    public class PublicationBakController : BaseWebController
    {
        PublicationService Service;
        PublicationLevel2Service Level2Serv;
        PublicationLevel3Service Level3Serv;

        public PublicationBakController()
        {
            Service = new PublicationService();
            Level2Serv = new PublicationLevel2Service();
            Level3Serv = new PublicationLevel3Service();
        }

        
        public ActionResult Index(PublicationListModel criteria)
        {
            try
            {
                PublicationListModel model = Service.GetList(string.Empty, criteria);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }

            return View(criteria);

        }

        public ActionResult Detail(String Id)
        {
            PublicationModel publication = Service.Get(null, Id);

            publication.Level2List = Level2Serv.GetList(null, new PublicationLevel2ListModel() { ParentId = publication.Id, PageSize = 1000 });


            if (publication.Level2List != null)
            {
                foreach (var Row in publication.Level2List.Data)
                {
                    Row.Level3List = Level3Serv.GetList(null, new PublicationLevel3ListModel() { ParentId = Row.Id, PageSize = 999999 });
                }
            }

            return View(publication);
        }
    }
}

