using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Models.Identity.ViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage ="用户名必须！")]
        [StringLength(20,MinimumLength =4,ErrorMessage ="用户名4-20个字符")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码长度必须是6-12位")]
        [StringLength(12, ErrorMessage = "密码长度6-12位", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不一致！")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }

        [Display(Name = "真实姓名")]
        public string RealName { get; set; }

        [Required(ErrorMessage ="验证码不能为空!")]
        [StringLength(6, ErrorMessage = "验证码长度6位", MinimumLength = 6)]
        public string ValidateCode { get; set; }
    }
}
