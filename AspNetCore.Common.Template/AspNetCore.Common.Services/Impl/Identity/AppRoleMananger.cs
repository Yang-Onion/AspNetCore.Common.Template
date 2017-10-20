
using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore.Common.Services.Identity.Impl
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IRoleStore<AppRole> store, IEnumerable<IRoleValidator<AppRole>> roleValidators,
               ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<AppRole>> logger)
               : base(store, roleValidators, keyNormalizer, errors, logger) {
        }

        public override Task<AppRole> FindByIdAsync(string roleId) {
            return Roles.Include(x => x.Claims).FirstOrDefaultAsync(r => r.Id.Equals(roleId));
        }

        public override Task UpdateNormalizedRoleNameAsync(AppRole role) {
            return Task.CompletedTask;
        }

        public List<AppRole> FindAllRoles() {
            var list = Roles.ToList();
            return list;
        }

        public List<AppRole> FindAllRoleByName(IList<string> names) {
            var list = Roles.Where(t => names.Contains(t.Name));
            return list.ToList();
        }

        //public async Task<IdentityResult> ModifyClaims(AppRole role, List<RoleMap> roleMap) {
        //    var oldRoleClaims = new List<string>();
        //    var newRoleClaims = new List<string>();
        //    foreach (var temp in role.Claims) {
        //        oldRoleClaims.Add(temp.ClaimValue);
        //    }
        //    foreach (var temp in roleMap) {
        //        newRoleClaims.Add(temp.MenuId);
        //    }
        //    var delTemp = oldRoleClaims.Except(newRoleClaims);//待删
        //    var addTemp = newRoleClaims.Except(oldRoleClaims);//待增
        //    IdentityResult returns = null;
        //    foreach (var a in delTemp) {
        //        returns = await RemoveClaimAsync(role, new Claim("Menu", a));
        //    }
        //    foreach (var a in addTemp) {
        //        returns = await AddClaimAsync(role, new Claim("Menu", a));
        //    }
        //    return returns;
        //}
    }
}