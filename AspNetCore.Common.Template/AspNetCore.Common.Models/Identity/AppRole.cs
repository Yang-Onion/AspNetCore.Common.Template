using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Models.Identity
{
    public class AppRole:IdentityRole<int>
    {
        public int RoleOrder { get; set; }
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; } = new List<IdentityRoleClaim<string>>();
    }
}
