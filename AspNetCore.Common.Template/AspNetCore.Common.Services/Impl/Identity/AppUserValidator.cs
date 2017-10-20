using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Common.Services.Identity.Impl
{
    public class AppUserValidator : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user) {
            var errors = new List<IdentityError>();
            await ValidatePhoneNumber(manager, user, errors);
            if (errors.Count > 0) {
                return IdentityResult.Failed(errors.ToArray());
            }
            else {
                return await base.ValidateAsync(manager, user);
            }
        }

        // make sure phone number is not empty, valid, and unique
        private async Task ValidatePhoneNumber(UserManager<AppUser> manager, AppUser user, List<IdentityError> errors) {
            var phoneUser = await manager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber);
            if (phoneUser != null &&
                !string.Equals(await manager.GetUserIdAsync(phoneUser), await manager.GetUserIdAsync(user))) {
                errors.Add(new IdentityError() { Code = "DuplicatePhone", Description = "手机号已经被使用。" });
            }
        }
    }
}