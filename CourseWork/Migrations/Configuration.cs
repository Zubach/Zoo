namespace CourseWork.Migrations
{
    using CourseWork.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CourseWork.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CourseWork.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.Roles.Any())
            {
                IdentityRole roleAdmin = new IdentityRole()
                {
                    Name = "Admin",
                };
                IdentityRole roleUser = new IdentityRole()
                {
                    Name = "User",
                };
                IdentityRole rolePimp = new IdentityRole()
                {
                    Name = "Pimp"
                };
                IdentityRole roleWhore = new IdentityRole() {
                    Name = "Whore"
                };
                roleManager.Create(roleAdmin);
                roleManager.Create(roleUser);
                roleManager.Create(rolePimp);
                roleManager.Create(roleWhore);
            }
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            if (!userManager.Users.Any())
            {
               
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "vanyakage@gmail.com",
                Email = "vanyakage@gmail.com",
            };

            userManager.Create(user, "Qwerty-1");
            userManager.AddToRole(user.Id, "Admin");

        }
    }
}
