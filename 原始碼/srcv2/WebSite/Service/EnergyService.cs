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

    public class EnergyService : BaseService
    {

        public List<DropItem> GetDropDownList(string PageName, string SelectedValue, string SelectName)
        {

            List<T_Energy> AllData = (from p in basedb.T_Energy
                        where p.PageName == PageName
                        orderby p.IOrder
                        select p).ToList();

            List<DropItem> DDL = Generate(AllData, null, SelectName, PageName);

            return DDL;
        }

        public List<DropItem> Generate (List<T_Energy> AllData, DropItem Parent, string SelectName, string PageName)
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
                dt.Depth = data[i].Depth.HasValue ? data[i].Depth.Value : -1;
                if (PageName == "常用能源指標" && dt.Depth == 0)
                {
                    dt.ShowCheckBox = false;
                }
                Generate(AllData, dt, SelectName, PageName);
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
        public EnergyModel NewInstance()
        {
            EnergyModel newOne = new EnergyModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public EnergyListModel GetList(string CurrentUserName, EnergyListModel Page)
        {
           var o_query = from p in basedb.T_Energy
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
                    new EnergyModel()
                    {
						Id = o_entity.Id,
						Pagename = o_entity.PageName,
						Parentid = o_entity.ParentId,
						Depth = o_entity.Depth,
						Name = o_entity.Name,
						Tablename = o_entity.TableName,
						Unitlistupper = o_entity.UnitListUpper,
						Unitlistbottom = o_entity.UnitListBottom,
						Colidlist = o_entity.ColIdList,
						Notes = o_entity.Notes,
						Hidelist = o_entity.HideList,
						Colorlist = o_entity.ColorList,
                        Iorder = o_entity.IOrder,
                        RowNo1 = o_entity.row_no1,
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, EnergyModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            T_Energy dbEntity = new T_Energy();
			dbEntity.Id = model.Id;
			dbEntity.PageName = model.Pagename;
			dbEntity.ParentId = model.Parentid;
			dbEntity.Depth = model.Depth;
			dbEntity.Name = model.Name;
			dbEntity.TableName = model.Tablename;
			dbEntity.UnitListUpper = model.Unitlistupper;
			dbEntity.UnitListBottom = model.Unitlistbottom;
			dbEntity.ColIdList = model.Colidlist;
			dbEntity.Notes = model.Notes;
			dbEntity.HideList = model.Hidelist;
			dbEntity.ColorList = model.Colorlist;
            dbEntity.IOrder = model.Iorder;
            dbEntity.row_no1 = model.RowNo1;

            basedb.T_Energy.Add(dbEntity);

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

		public EnergyModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Energy
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new EnergyModel()
                    {
						Id = o_entity.Id,
						Pagename = o_entity.PageName,
						Parentid = o_entity.ParentId,
						Depth = o_entity.Depth,
						Name = o_entity.Name,
						Tablename = o_entity.TableName,
						Unitlistupper = o_entity.UnitListUpper,
						Unitlistbottom = o_entity.UnitListBottom,
						Colidlist = o_entity.ColIdList,
						Notes = o_entity.Notes,
						Hidelist = o_entity.HideList,
						Colorlist = o_entity.ColorList,
                        Iorder = o_entity.IOrder,
                        RowNo1 = o_entity.row_no1,
                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, EnergyModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            try
            {
                T_Energy o_entity = new T_Energy()
                {
					Id = model.Id,
					PageName = model.Pagename,
					ParentId = model.Parentid,
					Depth = model.Depth,
					Name = model.Name,
					TableName = model.Tablename,
					UnitListUpper = model.Unitlistupper,
					UnitListBottom = model.Unitlistbottom,
					ColIdList = model.Colidlist,
					Notes = model.Notes,
					HideList = model.Hidelist,
					ColorList = model.Colorlist,
                    IOrder = model.Iorder,
                    row_no1 = model.RowNo1,
                };
				
                basedb.T_Energy.Attach(o_entity);

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

            var o_delete = from p in basedb.T_Energy
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Energy.Remove(row);
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