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
        public MenuViewComponent(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var temp = await;
            }
            return View();
        }

    }
}
