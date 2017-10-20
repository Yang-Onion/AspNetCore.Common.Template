using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Service.Identity.Impl
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher, IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<AppUser>> logger)
            : base(
                store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
                logger)
        {
        }

        public override Task<AppUser> FindByIdAsync(string userId)
        {
            var user = Users.Include(x => x.Roles).FirstOrDefaultAsync(u => u.Id.Equals(userId));
            return user;
        }

        public override Task<AppUser> FindByNameAsync(string normalizedUserName)
        {
            return Users.Include(x => x.Roles).FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
        }

        public bool IsUserNameUsed(string userName)
        {
            return Users.FirstOrDefault(x => x.UserName == userName) != null;
        }

        public bool IsPhoneNumberUsed(string phoneNumber)
        {
            return Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber) != null;
        }

        //public async Task AllocateRole(string userId, RoleEnum role)
        //{
        //    var user = await FindByIdAsync(userId);
        //    await AddToRoleAsync(user, role.GetDescription());
           
        //    var res = await UpdateAsync(user);

        //    if (!res.Succeeded)
        //        throw new DomainException("分配角色失败" + res.Errors.First().Description);
        //}

        //internal Task GetUserRoleStore()
        //{
        //    throw new NotImplementedException();
        //}

        //public PagedList<AppUser> FindAllAppUser(AppUserQueryDto appUserQueryDto)
        //{
        //    if (!string.IsNullOrEmpty(appUserQueryDto.UserName))
        //    {
        //        return Users.Where(g => g.UserName.Equals(appUserQueryDto.UserName)).ToPagedList(appUserQueryDto);
        //    }
        //    return Users.ToPagedList(appUserQueryDto);
        //}

        public AppUser FindAppUserByPhone(string phoneNumber)
        {
            return Users.FirstOrDefault(g => g.PhoneNumber.Equals(phoneNumber));
        }

        public override Task<bool> IsInRoleAsync(AppUser user, string role)
        {
            foreach (var identityUserRole in user.Roles)
            {
                Console.WriteLine(role);
            }
            return base.IsInRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRoles(AppUser user, List<AppRole> roleName, string roleId)
        {
            if (roleName.Count > 0)
            {
                var temp = new List<string>();
                roleName.ForEach(l =>
                {
                    temp.Add(l.NormalizedName);
                });
                var result = await RemoveFromRolesAsync(user, temp);
                if (result.Errors.Count() > 0)
                {
                    return result;
                }
            }

            user.Roles.Clear();

            if (!string.IsNullOrEmpty(roleId))
            {
                foreach (var a in roleId.Split(','))
                {
                    user.Roles.Add(new IdentityUserRole<string> { RoleId = a, UserId = user.Id });
                }
            }
            return await UpdateAsync(user);
        }
    }
}