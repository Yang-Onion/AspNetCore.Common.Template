using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Models.Identity
{
    public class AppUser: IdentityUser
    {
        /// <summary>
        /// 用户排序
        /// </summary>
        public int UserOrder { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
    }
}
