using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Common.Template.Data
{
    public class IdentityDbContext : IdentityDbContext<AppUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roleclaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userclaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userlogins");
            builder.Entity<IdentityUserRole<string>>().ToTable("userroles");
            builder.Entity<IdentityUserToken<string>>().ToTable("usertokens");
            builder.Entity<Menu>().ToTable("menus");

        }
    }
}
