﻿using AspNetCore.Common.Models.Identity.ViewModel;
using AspNetCore.Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AspNetCore.Common.Services.Identity.Impl
{
    public class MenuManager
    {
        private readonly IMenu _menu;
        private readonly AppRoleManager _roleManager;
        private readonly AppUserManager _userManager;

        public MenuManager(IMenu menu, AppUserManager userManager, AppRoleManager roleManager)
        {
            _menu = menu;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<MenuViewModel> GetUserMenuAsync(IPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return new MenuViewModel();
            }
            var userName = claimsPrincipal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var menuIds = new List<string>();
                foreach (var userRole in user.Roles)
                {
                    var role = await _roleManager.FindByIdAsync(userRole.RoleId);
                    var currentRolemenuIds = role.Claims.Select(x => x.ClaimValue);
                    menuIds.AddRange(currentRolemenuIds);
                }
                return _menu.CastMenuIdsToMenuModel(menuIds.Distinct());
            }
            return new MenuViewModel();
        }
    }
}