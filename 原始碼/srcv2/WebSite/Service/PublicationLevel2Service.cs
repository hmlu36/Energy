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
    public class PublicationLevel2Service : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public PublicationLevel2Model NewInstance()
        {
            PublicationLevel2Model newOne = new PublicationLevel2Model();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public PublicationLevel2ListModel GetList(string CurrentUserName, PublicationLevel2ListModel Page)
        {
           var o_query = from p in basedb.T_Publication_Level2
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
                             p.UpdateDate
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
                    new PublicationLevel2Model()
                    {
						Id = o_entity.Id,
						Parentid = o_entity.ParentId,
						Title = o_entity.Title,
						Iorder = o_entity.IOrder,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        ParentName = o_entity.ParentName,
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, PublicationLevel2Model model, out string ErrMsgs)
        {


            ErrMsgs = string.Empty;

            T_Publication_Level2 dbEntity = new T_Publication_Level2();
			dbEntity.Id = model.Id;
			dbEntity.ParentId = model.Parentid;
			dbEntity.Title = model.Title;
			dbEntity.IOrder = model.Iorder;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;


            basedb.T_Publication_Level2.Add(dbEntity);

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

		public PublicationLevel2Model Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Publication_Level2
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new PublicationLevel2Model()
                    {
						Id = o_entity.Id,
						Parentid = o_entity.ParentId,
						Title = o_entity.Title,
						Iorder = o_entity.IOrder,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,


                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, PublicationLevel2Model model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;



            try
            {
                T_Publication_Level2 o_entity = new T_Publication_Level2()
                {
					Id = model.Id,
					ParentId = model.Parentid,
					Title = model.Title,
                    ENTitle = model.ENTitle,
                    IOrder = model.Iorder,
					CreateDate = model.Createdate,
					UpdateDate = DateTime.Now,

                };
				
                basedb.T_Publication_Level2.Attach(o_entity);
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

            var o_delete = from p in basedb.T_Publication_Level2
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Publication_Level2.Remove(row);
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

        public List<SelectListItem> GetLv1List(string RoleId)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();

            var o_query = from p in basedb.T_Publication_Level1
                          
                          select new
                          {
                              p.Id,
                              p.Title,
                              p.ENTitle,
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