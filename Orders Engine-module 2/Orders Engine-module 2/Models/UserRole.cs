using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders_Engine_module_2.Models
{
    public class UserRole : IdentityRole
    {
        private string Description { get; set; }
        
        public UserRole() : base() { }
        public UserRole(string name) : base(name) { }

        public UserRole(string name, string v) : this(name)
        {
            this.Description = v;
        }
    }
}