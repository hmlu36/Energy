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
    public class ChildService : BaseService
    {
		/// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public ChildModel NewInstance()
        {
            ChildModel newOne = new ChildModel();
            //newOne.Id = Library.Utils.GetObjectId();
            //newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public ChildListModel GetList(string CurrentUserName, ChildListModel Page)
        {
           var o_query = from p in basedb.T_Child
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
            }

            if (!string.IsNullOrEmpty(Page.ParentId))
            {
                o_query = o_query.Where(p => p.ParentId == Page.ParentId);
            }

            o_query = o_query.Where(p => p.SelfId == Page.SelfId);

            var query = o_query.OrderBy(p => p.IOrder);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(o_entity =>
                    new ChildModel()
                    {
						Id = o_entity.Id,
						Industryid = o_entity.IndustryId,
						Parentid = o_entity.ParentId,
						Name = o_entity.Name,
						Iorder = o_entity.IOrder,
						Industryids = o_entity.IndustryIds,
						Columnidall = o_entity.ColumnIdAll,
						Columnid1 = o_entity.ColumnId1,
						Columnid2 = o_entity.ColumnId2,
						Columnid3 = o_entity.ColumnId3,
						Columnid4 = o_entity.ColumnId4,
						Columnid5 = o_entity.ColumnId5,
						Columnid6 = o_entity.ColumnId6,
						Columnid7 = o_entity.ColumnId7,
						Columnid8 = o_entity.ColumnId8,
						Columnid9 = o_entity.ColumnId9,
						Columnid10 = o_entity.ColumnId10,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        Selfid = o_entity.SelfId,
                        UnitList = o_entity.UnitList,
                        AltName = o_entity.AltName,
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

        

        public bool Create(string UserName, ChildModel model, out string ErrMsgs)
        {


            ErrMsgs = string.Empty;

            T_Child dbEntity = new T_Child();
			dbEntity.Id = model.Id;
			dbEntity.IndustryId = model.Industryid;
			dbEntity.ParentId = model.Parentid;
			dbEntity.Name = model.Name;
			dbEntity.IOrder = model.Iorder;
			dbEntity.IndustryIds = model.Industryids;
			dbEntity.ColumnIdAll = model.Columnidall;
			dbEntity.ColumnId1 = model.Columnid1;
			dbEntity.ColumnId2 = model.Columnid2;
			dbEntity.ColumnId3 = model.Columnid3;
			dbEntity.ColumnId4 = model.Columnid4;
			dbEntity.ColumnId5 = model.Columnid5;
			dbEntity.ColumnId6 = model.Columnid6;
			dbEntity.ColumnId7 = model.Columnid7;
			dbEntity.ColumnId8 = model.Columnid8;
			dbEntity.ColumnId9 = model.Columnid9;
			dbEntity.ColumnId10 = model.Columnid10;
			dbEntity.CreateDate = model.Createdate;
			dbEntity.UpdateDate = model.Updatedate;
            dbEntity.SelfId = model.Selfid;
            dbEntity.UnitList = model.UnitList;
            dbEntity.AltName = model.AltName;
            dbEntity.DecimalPlaces = model.DecimalPlaces;

            basedb.T_Child.Add(dbEntity);

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

		public ChildModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Child
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new ChildModel()
                    {
						Id = o_entity.Id,
						Industryid = o_entity.IndustryId,
						Parentid = o_entity.ParentId,
						Name = o_entity.Name,
						Iorder = o_entity.IOrder,
						Industryids = o_entity.IndustryIds,
						Columnidall = o_entity.ColumnIdAll,
						Columnid1 = o_entity.ColumnId1,
						Columnid2 = o_entity.ColumnId2,
						Columnid3 = o_entity.ColumnId3,
						Columnid4 = o_entity.ColumnId4,
						Columnid5 = o_entity.ColumnId5,
						Columnid6 = o_entity.ColumnId6,
						Columnid7 = o_entity.ColumnId7,
						Columnid8 = o_entity.ColumnId8,
						Columnid9 = o_entity.ColumnId9,
						Columnid10 = o_entity.ColumnId10,
						Createdate = o_entity.CreateDate,
						Updatedate = o_entity.UpdateDate,
                        Selfid = o_entity.SelfId,
                        UnitList = o_entity.UnitList,
                        AltName = o_entity.AltName,
                        DecimalPlaces = o_entity.DecimalPlaces,
                    };

                return model;
            }
            return null;
        }

		public bool Update(string userId, string userAccount, ChildModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            try
            {
                T_Child o_entity = new T_Child()
                {
                    Id = model.Id,
                    IndustryId = model.Industryid,
                    ParentId = model.Parentid,
                    Name = model.Name,
                    IOrder = model.Iorder,
                    IndustryIds = model.Industryids,
                    ColumnIdAll = model.Columnidall,
                    ColumnId1 = model.Columnid1,
                    ColumnId2 = model.Columnid2,
                    ColumnId3 = model.Columnid3,
                    ColumnId4 = model.Columnid4,
                    ColumnId5 = model.Columnid5,
                    ColumnId6 = model.Columnid6,
                    ColumnId7 = model.Columnid7,
                    ColumnId8 = model.Columnid8,
                    ColumnId9 = model.Columnid9,
                    ColumnId10 = model.Columnid10,
                    CreateDate = model.Createdate,
                    UpdateDate = DateTime.Now,
                    SelfId = model.Selfid,
                    UnitList = model.UnitList,
                    AltName = model.AltName,
                    DecimalPlaces = model.DecimalPlaces,
                };
				
                basedb.T_Child.Attach(o_entity);
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

            var o_delete = from p in basedb.T_Child
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Child.Remove(row);
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