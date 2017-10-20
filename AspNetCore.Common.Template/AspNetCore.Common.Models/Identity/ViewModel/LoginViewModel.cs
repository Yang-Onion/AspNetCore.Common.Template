using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCore.Common.Models.Identity.ViewModel
{
   public class LoginViewModel
   {
       [Required(ErrorMessage = "电子邮件不能为空。")]
       [EmailAddress(ErrorMessage = "电子邮件格式错误。")]
       public string Email { get; set; }

       [Required(ErrorMessage = "密码不能为空。")]
       [DataType(DataType.Password)]
       public string Password { get; set; }

       [Display(Name = "自动登录")]
       public bool RememberMe { get; set; }
   }
}
