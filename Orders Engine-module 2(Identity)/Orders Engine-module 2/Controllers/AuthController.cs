using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Orders_Engine_module_2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;

using System.Web.Mvc;
using System.Web.Security;

namespace Orders_Engine_module_2.Controllers
{
    public class AuthController : Controller
    {
        UserStore<AppUser> userStore;
        UserManager<AppUser> userManager;
        UsersContext<AppUser> dbuser = new UsersContext<AppUser>();

        // UserStore perform database storage and retrieval tasks
        public AuthController()
        {
            userStore = new UserStore<AppUser>(dbuser);
            userManager = new UserManager<AppUser>(userStore);
        }

        public ActionResult Register()
        {
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = new AppUser();      
                    var username = userManager.FindByName(model.UserName);
                    if (username != null)
                    {
                        ModelState.AddModelError("UserName", "Username Already in use");
                    }
                    else
                    {
                        user.UserName = model.UserName;
                        user.Email = model.Email;
                        user.FullName = model.FullName;

                        IdentityResult result = userManager.Create(user, model.Password);

                        if (result.Succeeded)
                        {
                            userManager.AddToRole(user.Id, "Customer");
                            return RedirectToAction("Login", "Auth");
                        }
                    }
                }
                catch
                {
                    TempData["Error"] = "There is no Internet connection";
                }
            }
            return View(model);
        }


        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //Return Login page view
        [HttpPost]
        public ActionResult Login(LoginModel model, String returnurl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = userManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    //props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
     
                    if (Url.IsLocalUrl(returnurl))
                    {
                        return Redirect(returnurl);
                    }
                    else
                    {
                        if (userManager.IsInRole(user.Id, "Administrator"))
                        {
                            Session["Administrator"] = true;
                            return RedirectToAction("DiscountOptions", "Discount");
                        }
                        else
                        {
                            Session["Customer"] = true;
                            return RedirectToAction("Homepage", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid username or password.");
                }

            }
            return View(model);
        }

        //[Authorize]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult ForgotPassword(ChangePassword model)
        //{
        //}

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = userManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = userManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", "Error while changing the password.");
                }
            }
            return View(model);
        }

        [Authorize]
       // [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Session["Administrator"] = false;
            Session["Customer"] = false;
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Auth");
        }
    }
}