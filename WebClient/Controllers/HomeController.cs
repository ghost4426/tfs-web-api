using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        [Authorize]
        [Route("trang-chu")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("dang-nhap")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            LoginModel login = new LoginModel() { Password = password, Username = username };
            var result = await login.CheckLogin();
            
            if (result.Data != null)
            {
                var data = result.Data;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, data.User.Username),
                    new Claim("FullName", data.User.Fullname),
                    new Claim("Image", data.User.Image),
                    new Claim(ClaimTypes.Role, data.User.Role)
                };

                if(data.User.Premises != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, data.User.Premises));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                Response.Cookies.Append("token", data.Token, new CookieOptions()
                {
                    IsEssential = true
                });
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        [Route("dat-lai-mat-khau")]
        public IActionResult RecoverPassword()
        {
            return View();
        }
        [Route("dang-ky-co-so")]
        public IActionResult CreatePremises()
        {
            return View();
        }

        [Route("tu-choi-quyen-truy-cap")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("kich-hoat-tai-khoan")]
        public IActionResult ActiveCode()
        {
            return View();
        }

        [Authorize]
        [Route("dang-xuat")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("token");
            return RedirectToAction("Login");
        }

    }
}
