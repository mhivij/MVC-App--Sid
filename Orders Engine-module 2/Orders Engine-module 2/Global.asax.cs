using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Orders_Engine_module_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Orders_Engine_module_2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        UsersContext dbuser = new UsersContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Database.SetInitializer<MyDbContext>(new MyDbInitializer());
            

            RoleStore<UserRole> roleStore = new RoleStore<UserRole>(dbuser);
            RoleManager<UserRole> roleManager = new RoleManager<UserRole>(roleStore);

            if (!roleManager.RoleExists("Administrator"))
            {
                UserRole newRole = new UserRole("Administrator", "Administrators can add, edit and delete data.");
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("Operator"))
            {
                UserRole newRole = new UserRole("Operator", "Operators can only add or edit data.");
                roleManager.Create(newRole);
            }
        }

    }
}
