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
    public class PublicationController : BaseWebController
    {
        PublicParentService Service = new PublicParentService();
        SystemService systemSrv = new SystemService();

        public PublicationController()
        {
        }

        
        public ActionResult Page0()
        {
            return View();
        }
        public ActionResult page_info()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("公告資訊");
            return View();
        }
        public ActionResult Page_info01()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("公告資訊");
            return View();
        }
        public ActionResult Page_info02()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("公告資訊");
            return View();
        }

        public ActionResult Page_info03()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("公告資訊");
            return View();
        }

        public ActionResult monthly(PublicListPage viewModel)
        {

            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("月報");

            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Parentid= "145de0e4732" });

            viewModel.Data = parentList;
            return View(viewModel);
        }
        public ActionResult monthly_detail(PublicListPage viewModel, string id)
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("月報");

            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Pageid = id });

            foreach (PublicParentModel parent in parentList.Data)
            {
                parent.Child = Service.GetListMonthlyDetail(null, new PublicDetailListModel() { Parentid = parent.Id });
                int o = 1;
            }
            viewModel.Data = parentList;
            return View(viewModel);
        }

        public ActionResult Page0new2()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("月報");
            return View();
        }

        public ActionResult Page0new_bk(PublicListPage viewModel)
        {
            
            
            
            
            
            PublicParentListModel parentList = Service.GetList(null, new PublicParentListModel()  );

            
            
            
            

            foreach (PublicParentModel parent in parentList.Data)
            {
                parent.Child = Service.GetListDetail(null, new PublicDetailListModel() { Parentid = parent.Id });
            }
            viewModel.Data = parentList;
           
            return View(viewModel);
        }

        
        public ActionResult Page01(PublicListPage viewModel)
        {
                ViewBag.LASTUPDATE = systemSrv.GetValueByKey("年報(平衡表)");
                PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Parentid = "2073f521515" });
                viewModel.Data = parentList;
                return View(viewModel);
        }
        public ActionResult Page01_detail(PublicListPage viewModel, string id)
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("年報(平衡表)");

            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Pageid = id });

            foreach (PublicParentModel parent in parentList.Data)
            {
                parent.Child = Service.GetListMonthlyDetail(null, new PublicDetailListModel() { Parentid = parent.Id });
                int o = 1;
            }
            viewModel.Data = parentList;
            return View(viewModel);
        }
        public ActionResult Page1()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("年報(平衡表)");
            return View();
        }

        public ActionResult Page2(PublicListPage viewModel)
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("手冊");
            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Parentid = "3875049306e" });
            viewModel.Data = parentList;
            return View(viewModel);
        }
        public ActionResult Page2_detail(PublicListPage viewModel, string id)
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("手冊");

            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Pageid = id });

            foreach (PublicParentModel parent in parentList.Data)
            {
                parent.Child = Service.GetListMonthlyDetail(null, new PublicDetailListModel() { Parentid = parent.Id });
                int o = 1;
            }
            viewModel.Data = parentList;
            return View(viewModel);
        }
        public ActionResult Page3()
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("燃料燃燒");

            return View();
        }

        public ActionResult Page4()
        {
            return View();
        }
        public ActionResult Page5(PublicListPage viewModel)
        {
            ViewBag.LASTUPDATE = systemSrv.GetValueByKey("手冊");
            PublicParentListModel parentList = Service.GetListMonthly(null, new PublicParentListModel() { Parentid = "3875049306e" });
            viewModel.Data = parentList;
            return View(viewModel);
        }
    }
}

