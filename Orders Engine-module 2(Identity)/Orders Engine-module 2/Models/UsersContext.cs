using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Orders_Engine_module_2.Models
{
    public class UsersContext<T> : IdentityDbContext<T>  where T : AppUser
    {
        public UsersContext(): base("DefaultConnection")
        {
        }
        
        public DbSet<LoginModel> UserProfiles { get; set; }

        //This error will occur if we will not use this function. "The model backing the 'UsersContext' context has changed since the database was created.Consider using Code First Migrations to update the database"
        //Use this funtion when implementing asp.net identity using code first with existing DB 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<UsersContext>().ToTable("Users");
            Database.SetInitializer<UsersContext<T>>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}