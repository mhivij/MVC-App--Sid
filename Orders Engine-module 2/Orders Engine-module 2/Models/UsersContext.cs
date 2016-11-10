using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Orders_Engine_module_2.Models
{
    public class UsersContext : IdentityDbContext<AppUser>
    {
        public UsersContext(): base("DefaultConnection")
        {
        }

        public DbSet<LoginModel> UserProfiles { get; set; }

        //Use this funtion when implementing asp.net identity using code first with existing DB 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<UsersContext>().ToTable("Users");
            Database.SetInitializer<UsersContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}