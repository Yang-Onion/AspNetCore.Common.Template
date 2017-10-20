using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCore.Common.Models.Identity;

namespace Cms.Service.Identity.Impl
{
    public class SignInManager : SignInManager<AppUser>
    {
        private readonly AppUserManager _userManager;
        private readonly string _signInScheme = IdentityConstants.ApplicationScheme;

        public SignInManager(AppUserManager userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager> logger, IAuthenticationSchemeProvider provider)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, provider)
        {
            _userManager = userManager;
        }

        public override async Task SignInAsync(AppUser user, bool isPersistent, string authenticationMethod = null)
        {
            //var needAddClaims = await GetNeedAddClaims(user);

            ////删除现有的Claims ，重新Add
            //var claims = await _userManager.GetClaimsAsync(user);
            //await _userManager.RemoveClaimsAsync(user, claims);

            //await _userManager.AddClaimsAsync(user, needAddClaims);
            var identity =new ClaimsPrincipal(await GenerateClaimsAsync(user));

            await Context.SignInAsync(_signInScheme, identity, new AuthenticationProperties());

            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        private async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var claimsIdentity = new ClaimsIdentity(_signInScheme, ClaimTypes.Name, ClaimTypes.Role);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            var claims = await GetNeedAddClaims(user);
            claimsIdentity.AddClaims(claims);
            return claimsIdentity; 
        }
        
        public override async Task SignOutAsync()
        {
            await Context.SignOutAsync(_signInScheme);
        }

        private async Task<IEnumerable<Claim>> GetNeedAddClaims(AppUser user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }

        public override async Task RefreshSignInAsync(AppUser user)
        {
            var needAddClaims = await GetNeedAddClaims(user);
            //删除现有的Claims ，重新Add
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);

            await _userManager.AddClaimsAsync(user, needAddClaims);
            await base.RefreshSignInAsync(user);
        }
    }
}