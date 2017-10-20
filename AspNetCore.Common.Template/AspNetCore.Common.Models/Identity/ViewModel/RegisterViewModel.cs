using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "密码长度6-12位", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不一致！")]
        public string ConfirmPassword { get; set; }        

        [Required]
        [MaxLength(30)]
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Phone]
        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }
    }
}
