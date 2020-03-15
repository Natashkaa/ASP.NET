using Microsoft.AspNet.Identity;
using ShopAdoWithCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ShopAdoWithCRUD.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login(string urlPath)
        {
            var model = new UserModel
            {
                Url = urlPath
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (!ModelState.IsValid) return View();

            if (user.Login == "admin" && user.Password == "admin")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Admin")
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignIn(identity);

                return Redirect(GetUrl(user.Url));
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }
        public ActionResult LogOut()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("login");
        }

        private string GetUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
            {
                return Url.Action("showgoods", "home");
            }
            return url;
        }
    }
}