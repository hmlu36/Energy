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

namespace JamZoo.Project.WebSite.Service
{
    public class PublicationService : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public PublicationModel NewInstance()
        {
            PublicationModel newOne = new PublicationModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public PublicationListModel GetList(string CurrentUserName, PublicationListModel Page)
        {
           var o_query = from p in basedb.T_Publication
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
                o_query = o_query.Where(p => p.Title.Contains(Page.Search));
            }

            var query = o_query.OrderBy(p => p.IOrder);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new PublicationModel()
                    {
						Id = o_entity.Id,
						Title = o_entity.Title,
						Image = o_entity.Image,
						Url = o_entity.Url,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        Allfile = o_entity.Allfile,
                        Iorder = o_entity.IOrder,
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, PublicationModel model, out string ErrMsgs)
        {
            if (model.ImageFile != null)
            {
                Library.Utils.SaveFile<PublicationModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Image", model.ImageFile);
            }

            if (model.AllfileFile != null)
            {
                Library.Utils.SaveFile<PublicationModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Allfile", model.AllfileFile);
            }


            ErrMsgs = string.Empty;

            T_Publication dbEntity = new T_Publication();
			dbEntity.Id = model.Id;
			dbEntity.Title = model.Title;
			dbEntity.Image = model.Image;
			dbEntity.Url = model.Url;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;
            dbEntity.Allfile = model.Allfile;
            dbEntity.IOrder = model.Iorder;

            basedb.T_Publication.Add(dbEntity);

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

		public PublicationModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Publication
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new PublicationModel()
                    {
						Id = o_entity.Id,
						Title = o_entity.Title,
						Image = o_entity.Image,
						Url = o_entity.Url,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        Allfile = o_entity.Allfile,
                        Iorder = o_entity.IOrder
                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, PublicationModel model, out string ErrMsgs)
        {
            if (model.ImageFile != null)
            {
                Library.Utils.SaveFile<PublicationModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Image", model.ImageFile);
            }

            if (model.AllfileFile != null)
            {
                Library.Utils.SaveFile<PublicationModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Allfile", model.AllfileFile);
            }

            ErrMsgs = string.Empty;

            try
            {
                T_Publication o_entity = new T_Publication()
                {
					Id = model.Id,
					Title = model.Title,
					Image = model.Image,
					Url = model.Url,
                    Allfile = model.Allfile,
					CreateDate = model.Createdate,
					UpdateDate = DateTime.Now,
                    IOrder = model.Iorder,
                };
				
                basedb.T_Publication.Attach(o_entity);
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

            var o_delete = from p in basedb.T_Publication
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Publication.Remove(row);
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

        
   }
}