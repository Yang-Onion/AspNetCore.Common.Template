using AspNetCore.Common.Infrastructure.Interface;
using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Data
{
    public class IdentityDbContext : IdentityDbContext<AppUser,AppRole,string>, IIdentityDbContext,IUnitOfWork
    {
        public readonly IConfiguration _config;
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options,IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }
  

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UserOrganizations> UserOrganizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_config.GetConnectionString("IdentityDbConnection"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("users")
                .HasMany(t=>t.Roles)
                .WithOne()
                .HasForeignKey(t=>t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AppRole>().ToTable("roles")
                .HasMany(t => t.Claims)
                .WithOne()
                .HasForeignKey(t => t.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roleclaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userclaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userlogins");
            builder.Entity<IdentityUserRole<string>>().ToTable("userroles");
            builder.Entity<IdentityUserToken<string>>().ToTable("usertokens");
            builder.Entity<Menu>().ToTable("menus");
            builder.Entity<Organization>().ToTable("organization");
            builder.Entity<UserOrganizations>().ToTable("userorganizations");
            
        }

        public int Commit()
        {
            return SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return SaveChangesAsync();
        }

        public IList<T> Sql<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
