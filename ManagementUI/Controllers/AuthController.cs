using ApplicationCore.Interfaces;
using ManagementUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.LoginAsync(model.Account, model.Password);
                    if (result != null)
                    {
                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, result.Account),
                            new Claim(ClaimTypes.Role, result.Role),
                            //new Claim(ClaimTypes.NameIdentifier, result.Account),
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(model);
            }
            catch (Exception e)
            {
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
