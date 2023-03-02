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
    public class ParentService : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public ParentModel NewInstance()
        {
            ParentModel newOne = new ParentModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public ParentListModel GetList(string CurrentUserName, ParentListModel Page)
        {
           var o_query = from p in basedb.T_Parent
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
            }

            if (!string.IsNullOrEmpty(Page.EnergyName))
            {
                o_query = o_query.Where(p => p.EnergyName == Page.EnergyName);
            }

            var query = o_query.OrderBy(p => p.IOrder);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new ParentModel()
                    {
                        Id = o_entity.Id,
                        Energyname = o_entity.EnergyName,
                        Name = o_entity.Name,
                        Iorder = o_entity.IOrder,
                        Createdate = o_entity.CreateDate,
                        Updatedate = o_entity.UpdateDate,
                        Color = o_entity.Color,
                        UnitList = o_entity.UnitList,
                        UnitListUpper = o_entity.UnitListUpper,
                        HiddenChart = o_entity.HiddenChart,
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        

        public bool Create(string UserName, ParentModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            T_Parent dbEntity = new T_Parent();
			dbEntity.Id = model.Id;
			dbEntity.EnergyName = model.Energyname;
			dbEntity.Name = model.Name;
			dbEntity.IOrder = model.Iorder;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;
            dbEntity.Color = model.Color;
            dbEntity.UnitList = model.UnitList;
            dbEntity.HiddenChart = model.HiddenChart;
            dbEntity.UnitListUpper = model.UnitListUpper;

            basedb.T_Parent.Add(dbEntity);

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

		public ParentModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Parent
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new ParentModel()
                    {
						Id = o_entity.Id,
						Energyname = o_entity.EnergyName,
						Name = o_entity.Name,
						Iorder = o_entity.IOrder,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        Color = o_entity.Color,
                        UnitList = o_entity.UnitList,
                        HiddenChart = o_entity.HiddenChart,
                        UnitListUpper = o_entity.UnitListUpper,
                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, ParentModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            try
            {
                T_Parent o_entity = new T_Parent()
                {
                    Id = model.Id,
                    EnergyName = model.Energyname,
                    Name = model.Name,
                    IOrder = model.Iorder,
                    CreateDate = model.Createdate,
                    UpdateDate = DateTime.Now,
                    Color = model.Color,
                    UnitList = model.UnitList,
                    HiddenChart = model.HiddenChart,
                    UnitListUpper = model.UnitListUpper,
                };
				
                basedb.T_Parent.Attach(o_entity);
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

            var o_delete = from p in basedb.T_Parent
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Parent.Remove(row);
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