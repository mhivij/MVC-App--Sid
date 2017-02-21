using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders_Engine_module_2.Models
{
    //Represents dbo.AspNetUsers table
    //The IdentityUser base class provides several properties such as UserName, Email and PasswordHash
    public class AppUser: IdentityUser
    {
        //Added a new property Fullname
        public string FullName { get; set; }
    }
}