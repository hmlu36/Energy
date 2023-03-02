using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JamZoo.Project.WebSite.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using JamZoo.Project.WebSite.Mvc;

namespace JamZoo.Project.WebSite.Models
{
    /// <summary>
    /// 列表頁
    /// </summary>
    public class AccountListModel : PagerModel
    {
        public string Search { get; set; }

        public List<AccountModel> Data { get; set; }
    }
    /// <summary>
    /// 帳號
    /// </summary>
    public class AccountModel : EditModePage
    {
        public string IP { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage="必填")]
        [Display(Name = "帳號")]
        [CsvField(DisplayName = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "與密碼不相符")]
        public string RePassword { get; set; }

        [CsvField(DisplayName = "狀態")]
        [Display(Name = "狀態")]
        public int Status { get; set; }


        [CsvField(DisplayName = "建立時間")]
        [Display(Name = "建立時間")]
        public DateTime CreateDate { get; set; }

        [CsvField(DisplayName = "最後登入時間")]
        [Display(Name = "最後登入時間")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 取得 Role
        /// </summary>
        /// <returns></returns>
        [Display(Name = "角色")]
        public string Role { get; set; }
       
        /// <summary>
        /// 取得 Role
        /// </summary>
        /// <returns></returns>
        [Display(Name = "角色")]
        public List<string> RoleList { get; set; }

        public List<SelectListItem> RoleSelectList { get; set; }

        /// <summary>
        /// 網站角色判斷
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public bool IsInRoles(List<string> _Roles)
        {
            if (_Roles == null) return false;
            if (_Roles.Count() == 0) return false;
            foreach (var Role in _Roles)
            {
                if (RoleList != null)
                {
                    if (RoleList.Contains(Role))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}