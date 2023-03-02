/*
ModelName
dbModelName
KeyFieldType
KeyEexpression
DaoKeyFieldName
UpdateFields
Mapping_Fields_A
Mapping_Fields_B
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data;
using System.Data.Objects;

using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.Extensions;
using JamZoo.Project.WebSite.DbContext;
using System.Web.Mvc;

namespace JamZoo.Project.WebSite.Service
{
    public class PublicationLevel3Service : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public PublicationLevel3Model NewInstance()
        {
            PublicationLevel3Model newOne = new PublicationLevel3Model();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public PublicationLevel3ListModel GetList(string CurrentUserName, PublicationLevel3ListModel Page)
        {
           var o_query = from p in basedb.T_Publication_Level3
                         from o in basedb.T_Publication_Level1
                         where o.Id == p.ParentId
                         select new
                         {
                             ParentName = o.Title,
                             p.Title,
                             p.ENTitle,

                             p.ParentId,
                             p.IOrder,
                             p.Id,
                             p.CreateDate,
                             p.UpdateDate,
                             p.Ods,
                             p.Pdf,
                             p.Word,
                             p.Excel,
                             p.Meta,
                             p.PNG,
				p.JSON
                         };

            if (!string.IsNullOrEmpty(Page.Search))
            {
                o_query = o_query.Where(p => p.Title.Contains(Page.Search));
            }

            if (!string.IsNullOrEmpty(Page.ParentId))
            {
                o_query = o_query.Where(p => p.ParentId == Page.ParentId);
            }

            var query = o_query.OrderBy(p => p.IOrder);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new PublicationLevel3Model()
                    {
						Id = o_entity.Id,
						Parentid = o_entity.ParentId,
						Title = o_entity.Title,
                        ENTitle = o_entity.ENTitle,

                        Iorder = o_entity.IOrder,
						Ods = o_entity.Ods,
						Pdf = o_entity.Pdf,
						Word = o_entity.Word,
						Meta = o_entity.Meta,
						Excel = o_entity.Excel,
                        PNG = o_entity.PNG,
                        JSON  = o_entity.JSON ,                        	
                        Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        ParentName = o_entity.ParentName
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, PublicationLevel3Model model, out string ErrMsgs)
        {
            if (model.OdsFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Ods", model.OdsFile, true);
            }

            if (model.PdfFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Pdf", model.PdfFile, true);
            }

            if (model.WordFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Word", model.WordFile, true);
            }
            if (model.MetaFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Meta", model.MetaFile, true);
            }
            if (model.ExcelFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Excel", model.ExcelFile, true);
            }
            if (model.PNGFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "PNG", model.PNGFile, true);
            }
            if (model.JSONFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "JSON", model.JSONFile, true);
            }
            ErrMsgs = string.Empty;

            T_Publication_Level3 dbEntity = new T_Publication_Level3();
			dbEntity.Id = model.Id;
			dbEntity.ParentId = model.Parentid;
			dbEntity.Title = model.Title;
            if (model.ENTitle == null) model.ENTitle = "";
            dbEntity.ENTitle = model.ENTitle;

            dbEntity.IOrder = model.Iorder;
			dbEntity.Ods = model.Ods;
			dbEntity.Pdf = model.Pdf;
			dbEntity.Word = model.Word;
			dbEntity.Meta = model.Meta;
			dbEntity.Excel = model.Excel;
            dbEntity.PNG = model.PNG;
		dbEntity.JSON  = model.JSON ;
            dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;


            basedb.T_Publication_Level3.Add(dbEntity);

            try
            {

                basedb.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrMsgs = ex.InnerException.Message;
                }
                else
                {
                    ErrMsgs = ex.Message;
                }
            }

            return ErrMsgs.Length == 0;
        }

		public PublicationLevel3Model Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Publication_Level3
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new PublicationLevel3Model()
                    {
						Id = o_entity.Id,
						Parentid = o_entity.ParentId,
						Title = o_entity.Title,
                    ENTitle = o_entity.ENTitle,

                    Iorder = o_entity.IOrder,
						Ods = o_entity.Ods,
						Pdf = o_entity.Pdf,
						Word = o_entity.Word,
						Meta = o_entity.Meta,
						Excel = o_entity.Excel,
                        PNG = o_entity.PNG,
                                                JSON  = o_entity.JSON,
                        Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,

                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, PublicationLevel3Model model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;
            if (model.ENTitle == null) model.ENTitle = "";
            if (model.OdsFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Ods", model.OdsFile, true);
            }

            if (model.PdfFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Pdf", model.PdfFile, true);
            }

            if (model.WordFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Word", model.WordFile, true);
            }
            if (model.MetaFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Meta", model.MetaFile, true);
            }
            if (model.ExcelFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Excel", model.ExcelFile, true);
            }
            if (model.PNGFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "PNG", model.PNGFile, true);
            }
            if (model.JSONFile != null)
            {
                Library.Utils.SaveFile<PublicationLevel3Model>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "JSON", model.JSONFile, true);
            }

            try
            {
                T_Publication_Level3 o_entity = new T_Publication_Level3()
                {
					Id = model.Id,
					ParentId = model.Parentid,
					Title = model.Title,
                    ENTitle = model.ENTitle,

                    IOrder = model.Iorder,
					Ods = model.Ods,
					Pdf = model.Pdf,
					Word = model.Word,
					Meta = model.Meta,
					Excel = model.Excel,
                    PNG = model.PNG,
			JSON = model.JSON,
                    CreateDate = model.Createdate,
					UpdateDate = DateTime.Now,

                };
				
                basedb.T_Publication_Level3.Attach(o_entity);
				basedb.Entry(o_entity).Property(x => x.CreateDate).IsModified = true;

                basedb.Entry(o_entity).State = EntityState.Modified;
                basedb.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ErrMsgs = ex.InnerException.Message;
                }
                else
                {
                    ErrMsgs = ex.Message;
                }
            }

            return ErrMsgs.Length == 0;    
        }

        public bool Delete(string userName, string id, out string ErrorMsg)
        {
            ErrorMsg = string.Empty;

            var o_delete = from p in basedb.T_Publication_Level3
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Publication_Level3.Remove(row);
            }

            try
            {
                basedb.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return ErrorMsg.Length == 0;
        }
        public int GetOrderNum()
        {
            var o_query = from p in basedb.T_Publication_Level3
                          from o in basedb.T_Publication_Level1
                          where o.Id == p.ParentId
                          select new
                          {
                              ParentName = o.Title,
                              p.Title,
                              p.ParentId,
                              p.IOrder,
                              p.Id,
                              p.CreateDate,
                              p.UpdateDate,
                              p.Ods,
                              p.Pdf,
                              p.Word,
                              p.Excel,
                              p.Meta

                          };

   
                return o_query.Select(p => p.IOrder).Max();
        }

        public List<SelectListItem> GetLv2List(string RoleId)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();

            var o_query = from p in basedb.T_Publication_Level1

                          select new
                          {
                              p.Id,
                              p.Title,
                              p.IOrder
                          };
            o_query = o_query.OrderBy(p => p.IOrder);
            foreach (var p in o_query)
            {
                ItemList.Add(new SelectListItem()
                {
                    Text = p.Title
,
                    Value = p.Id,
                    Selected = p.Id.Equals(RoleId)
                });
            }

            if (string.IsNullOrEmpty(RoleId))
            {
                //ItemList.Add(new SelectListItem()
                //{
                //    Text = "",
                //    Value = "",
                //    Selected = true
                //});
            }

            return ItemList.ToList();
        }
    }
}