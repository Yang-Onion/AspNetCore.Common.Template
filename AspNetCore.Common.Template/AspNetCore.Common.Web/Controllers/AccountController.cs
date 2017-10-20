using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCore.Common.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using AspNetCore.Common.Models.Identity.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AlpineSkiHouse.Web.Models.AccountViewModels;
using AspNetCore.Common.Infrastructure.Web;
using Microsoft.Extensions.Caching.Distributed;
using AspNetCore.Common.Services.Interface;
using AspNetCore.Common.Infrastructure.Extension;

namespace AspNetCore.Common.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;
        public  readonly IDistributedCache _cache;
        private readonly ISmsSender _smsSender;
        public AccountController(SignInManager<AppUser> signInManager, ILogger<AccountController> logger, UserManager<AppUser> userManager,IDistributedCache cache, ISmsSender smsSender)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _cache = cache;
            _smsSender = smsSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "账户被锁定,请联系管理员！");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError("","用户名密码不正确");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                //    // Send an email with this link
                //    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    _logger.LogInformation(3, "User created a new account with password.");
                //    return RedirectToLocal(returnUrl);
                //}
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
           var number = User.AsAppUser().PhoneNumber;
            var phoneCode = new Random().Next(100000, 999999);
            //_cache.SetInt32(AppKeys.VERFIY_CODE + number, phoneCode, new TimeSpan(0, 10, 0));
            _cache.SetInt32("VERTIFY_CODE_" + number, phoneCode, new TimeSpan(0, 10, 0));
            await _smsSender.SendSmsAsync(number, "你本次操作验证码为：" + phoneCode + "，有效期10分钟，请不要泄露给别人。");
            return Json(null);
        }
    }
}
