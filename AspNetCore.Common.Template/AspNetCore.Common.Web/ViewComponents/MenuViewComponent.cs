using AspNetCore.Common.Infrastructure;
using AspNetCore.Common.Infrastructure.Extension;
using AspNetCore.Common.Infrastructure.Web;
using AspNetCore.Common.Models.Identity.ViewModel;
using AspNetCore.Common.Services.Identity.Impl;
using AspNetCore.Common.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Web.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IDistributedCache _cache;
        private readonly MenuManager _menuManager;
        public MenuViewComponent(IDistributedCache cache, MenuManager menuManager)
        {
            _cache = cache;
            _menuManager = menuManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = UserClaimsPrincipal.GetUserId();
                var cacheKey = string.Format(AppKeys.COMMON_MENU, userId);
                var menuViewModel = _cache.Get<MenuViewModel>(cacheKey);
                if (menuViewModel == null)
                {
                    menuViewModel = await _menuManager.GetUserMenuAsync(User);
                    await _cache.SetAsync(cacheKey, menuViewModel, TimeSpan.FromMinutes(60));
                }
            }
            return View();
        }
    }
}
