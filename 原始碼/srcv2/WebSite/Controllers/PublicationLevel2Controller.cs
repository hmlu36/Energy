﻿/*
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
    public class PublicationLevel2Controller : BaseController
    {
        PublicationLevel2Service Service;

        public PublicationLevel2Controller()
        {
            Service = new PublicationLevel2Service();
        }

        #region CRUD
        
        public ActionResult List(PublicationLevel2ListModel criteria)
        {
            try
            {
                PublicationLevel2ListModel model = Service.GetList(User.Identity.Name, criteria);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }

            return View(criteria);
        }

        public ActionResult Add(int page = 1)
        {
            PublicationLevel2Model entity = Service.NewInstance();

            entity.page = page;
            entity.ParentSelectList = Service.GetLv1List("");
            entity.Mode = EditPageMode.Create;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Create(PublicationLevel2Model model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ErrMsgs = string.Empty;

                    if (Service.Create(
                        User.Identity.Name, 
                        model, out ErrMsgs))
                    {
                        return RedirectToAction("List", new { page = model.page });
                    }

                    ModelState.AddModelError("message", ErrMsgs);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("message", ex.Message);
                }
            }

            model.Mode = EditPageMode.Create;
            return View("Add", model);
        }


        public ActionResult Edit(string id, int page = 1)
        {
            PublicationLevel2Model model = Service.Get(User.Identity.Name, id);
			if (model != null)
			{
				model.Mode = EditPageMode.Update;
                model.ParentSelectList = Service.GetLv1List(model.Parentid); 
                

                return View("Add", model);
			}
			else
			{
				return Content("無此資料");
			}
        }

        [HttpPost]
        public ActionResult Update(PublicationLevel2Model model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ErrMsgs = string.Empty;

					string UserAccount = (null != UserManager.User) ? UserManager.User.Account : "N/A";

                    if (Service.Update(User.Identity.Name, UserAccount, model, out ErrMsgs))
                    {
                        return RedirectToAction("List", new { page = model.page });
                    }
                    else
                    {
                        ModelState.AddModelError("message", "修改失敗:" + ErrMsgs);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("message", ex.Message);
                }
            }

            model.Mode = EditPageMode.Update;
            return View("Add", model);
        }

        public ActionResult Delete(string id, int page = 1)
        {
            try
            {
                string errorMsg = string.Empty;

                if (Service.Delete(User.Identity.Name, id, out errorMsg))
                {
                    TempData["delete"] = true;
                }
                return RedirectToAction("List", new { page = page });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }
            return RedirectToAction("List");
        }
        #endregion
       

    }
}

