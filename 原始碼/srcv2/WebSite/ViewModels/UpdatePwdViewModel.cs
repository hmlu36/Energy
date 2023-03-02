using System.ComponentModel.DataAnnotations;

namespace JamZoo.Project.WebSite.ViewModels
{
    public class UpdatePwdViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        
        
        
        public string Password { get; set; }

        
        
        
        [DataType(DataType.Password)]
        [Display(Name = "確認新密碼")]
        [Compare("Password", ErrorMessage = "新密碼與確認密碼不相符。")]
        public string RePassword { get; set; }
    }
}
