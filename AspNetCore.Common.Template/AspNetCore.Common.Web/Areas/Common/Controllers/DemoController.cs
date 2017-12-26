using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Common.Web.Areas.Common.Controllers
{
    public class DemoController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
