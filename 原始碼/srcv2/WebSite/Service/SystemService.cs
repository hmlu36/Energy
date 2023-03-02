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
    public class SystemService : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public SystemModel NewInstance()
        {
            SystemModel newOne = new SystemModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public SystemListModel GetList(string CurrentUserName, SystemListModel Page)
        {
           var o_query = from p in basedb.T_System
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
            }

            var query = o_query.OrderBy(p => p.Id);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new SystemModel()
                    {
						Id = o_entity.Id,
						Mk = o_entity.Mk,
						Mv = o_entity.Mv,
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

        

        public bool Create(string UserName, SystemModel model, out string ErrMsgs)
        {

            ErrMsgs = string.Empty;

            T_System dbEntity = new T_System();
			dbEntity.Id = model.Id;
			dbEntity.Mk = model.Mk;
			dbEntity.Mv = model.Mv;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;


            basedb.T_System.Add(dbEntity);

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

        public void 瀏覽人次加一()
        {
            var o_entity = (from p in basedb.T_System
                            where p.Mk == "瀏覽人次"
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                int newCount = 0;
                if (Int32.TryParse(o_entity.Mv, out newCount))
                {
                    newCount += 1;

                    o_entity.Mv = newCount.ToString();
                    basedb.SaveChanges();
                }
            }
        }

        public string GetValueByKey(string Key)
        {
            var o_entity = (from p in basedb.T_System
                            where p.Mk == Key
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new SystemModel()
                {
                    Id = o_entity.Id,
                    Mk = o_entity.Mk,
                    Mv = o_entity.Mv,
                    Createdate = o_entity.CreateDate,
                    Updatedate = o_entity.UpdateDate,

                };

                return model.Mv;
            }
            return null;
        }

        public SystemModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_System
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new SystemModel()
                    {
						Id = o_entity.Id,
						Mk = o_entity.Mk,
						Mv = o_entity.Mv,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,

                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, SystemModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;



            try
            {
                T_System o_entity = new T_System()
                {
					Id = model.Id,
					Mk = model.Mk,
					Mv = model.Mv,
					CreateDate = model.Createdate,
					UpdateDate = DateTime.Now,

                };
				
                basedb.T_System.Attach(o_entity);
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

            var o_delete = from p in basedb.T_System
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_System.Remove(row);
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