using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JamZoo.Project.WebSite.Enums;

namespace JamZoo.Project.WebSite.ViewModels
{
    
    
    
    public class LoginPage
    {
        [Required]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string P1 { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

}
