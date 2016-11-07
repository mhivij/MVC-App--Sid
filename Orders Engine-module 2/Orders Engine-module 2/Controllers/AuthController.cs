using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class AuthController : Controller
    {
        // GET: Login
        public ActionResult RedirectToCustomer()
        {
            return View();
        }
        /* public ActionResult Login(string returnUrl)
         {
             var model = new LoginModel
             {
                 ReturnUrl = returnUrl
             };

             return View(model);
         }*/
         //Return Login page view
        public ActionResult Login(string submit)
        {
            return View();
        }

    }
}