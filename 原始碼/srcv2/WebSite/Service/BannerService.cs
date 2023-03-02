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
    public class BannerService : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public BannerModel NewInstance()
        {
            BannerModel newOne = new BannerModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public BannerListModel GetList(string CurrentUserName, BannerListModel Page)
        {
           var o_query = from p in basedb.T_Banner
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
            }

            var query = o_query.OrderByDescending(p => p.CreateDate);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new BannerModel()
                    {
						Id = o_entity.Id,
						Title = o_entity.Title,
						Img = o_entity.Img,
						Url = o_entity.Url,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,

                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, BannerModel model, out string ErrMsgs)
        {
            if (model.ImgFile != null)
            {
                Library.Utils.SaveFile<BannerModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Img", model.ImgFile);
            }

            ErrMsgs = string.Empty;

            T_Banner dbEntity = new T_Banner();
			dbEntity.Id = model.Id;
			dbEntity.Title = model.Title;
			dbEntity.Img = model.Img;
			dbEntity.Url = model.Url;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;


            basedb.T_Banner.Add(dbEntity);

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

		public BannerModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Banner
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new BannerModel()
                    {
						Id = o_entity.Id,
						Title = o_entity.Title,
						Img = o_entity.Img,
						Url = o_entity.Url,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,

                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, BannerModel model, out string ErrMsgs)
        {
            if (model.ImgFile != null)
            {
                Library.Utils.SaveFile<BannerModel>(model,
                    HttpContext.Current.Server.MapPath("~/Upload"), "Img", model.ImgFile);
            }

            ErrMsgs = string.Empty;

            try
            {
                T_Banner o_entity = new T_Banner()
                {
					Id = model.Id,
					Title = model.Title,
					Img = model.Img,
					Url = model.Url,
					CreateDate = model.Createdate,
					UpdateDate = DateTime.Now,

                };
				
                basedb.T_Banner.Attach(o_entity);
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

            var o_delete = from p in basedb.T_Banner
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Banner.Remove(row);
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