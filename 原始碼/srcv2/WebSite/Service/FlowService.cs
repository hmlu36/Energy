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
    using ViewModels;

    public class FlowService : BaseService
    {
        public List<DropItem> GetDropDownList(string PageName, string SelectedValue, string SelectName)
        {

            List<T_Flow> AllData = (from p in basedb.T_Flow
                                      where p.PageName == PageName
                                      orderby p.IOrder
                                      select p).ToList();

            List<DropItem> DDL = Generate(AllData, null, SelectName);

            return DDL;
        }

        public List<DropItem> Generate(List<T_Flow> AllData, DropItem Parent, string SelectName)
        {
            string strParentId = Parent != null ? Parent.Id : "";
            List<DropItem> rootList = Parent == null ? new List<DropItem>() : null;

            var data = (from p in AllData
                        where p.ParentId == strParentId
                        orderby p.IOrder
                        select p).ToList();

            for (int i = 0; i < data.Count(); i++)
            {
                DropItem dt = new DropItem(SelectName);
                dt.Id = data[i].Id;
                dt.Name = data[i].Name;
                dt.Depth = data[i].Depth;
                Generate(AllData, dt, SelectName);
                if (Parent != null)
                {
                    Parent.ChildList.Add(dt);
                }

                if (rootList != null)
                {
                    rootList.Add(dt);
                }
            }
            return rootList;
        }

        /// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public FlowModel NewInstance()
        {
            FlowModel newOne = new FlowModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public FlowListModel GetList(string CurrentUserName, FlowListModel Page)
        {
           var o_query = from p in basedb.T_Flow
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
            }

            if (Page.SelectedValues != null)
            {
                o_query = from p in o_query
                          where Page.SelectedValues.Contains(p.Id)
                          select p;
            }

            var query = o_query.OrderBy(p => p.IOrder);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new FlowModel()
                    {
						Id = o_entity.Id,
						Pagename = o_entity.PageName,
						Parentid = o_entity.ParentId,
						Depth = o_entity.Depth,
						Name = o_entity.Name,
						RowNo1 = o_entity.row_no1,
                        Iorder = o_entity.IOrder,
                        Colidlist = o_entity.ColIdList,
                        DecimalPlaces = o_entity.DecimalPlaces
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, FlowModel model, out string ErrMsgs)
        {

            ErrMsgs = string.Empty;

            T_Flow dbEntity = new T_Flow();
			dbEntity.Id = model.Id;
			dbEntity.PageName = model.Pagename;
			dbEntity.ParentId = model.Parentid;
			dbEntity.Depth = model.Depth;
			dbEntity.Name = model.Name;
			dbEntity.row_no1 = model.RowNo1;
            dbEntity.IOrder = model.Iorder;
            dbEntity.ColIdList = model.Colidlist;
            dbEntity.DecimalPlaces = model.DecimalPlaces;

            basedb.T_Flow.Add(dbEntity);

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

		public FlowModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Flow
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new FlowModel()
                    {
						Id = o_entity.Id,
						Pagename = o_entity.PageName,
						Parentid = o_entity.ParentId,
						Depth = o_entity.Depth,
						Name = o_entity.Name,
						RowNo1 = o_entity.row_no1,
                        Iorder = o_entity.IOrder,
                        Colidlist = o_entity.ColIdList,
                        DecimalPlaces = o_entity.DecimalPlaces,
                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, FlowModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            try
            {
                T_Flow o_entity = new T_Flow()
                {
					Id = model.Id,
					PageName = model.Pagename,
					ParentId = model.Parentid,
					Depth = model.Depth,
					Name = model.Name,
					row_no1 = model.RowNo1,
                    IOrder = model.Iorder,
                    DecimalPlaces = model.DecimalPlaces,
                };
				
                basedb.T_Flow.Attach(o_entity);

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

            var o_delete = from p in basedb.T_Flow
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Flow.Remove(row);
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