namespace BlankAPI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlankAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BlankAPI.Models.ApplicationDbContext";
        }

        protected override void Seed(BlankAPI.Models.ApplicationDbContext context)
        {
            var rolestore = new RoleStore<IdentityRole>(context);
            var rolemanager = new RoleManager<IdentityRole>(rolestore);

            if (!context.Roles.Any(r => r.Name == "admin"))
            {

                var role = new IdentityRole { Name = "admin" };

                rolemanager.Create(role);
            }
            var userstore = new UserStore<ApplicationUser>(context);
            var usermanager = new UserManager<ApplicationUser>(userstore);

            if (!context.Users.Any(u => u.UserName == "Kolbeinn"))
            {

                var user = new ApplicationUser { Email = "kolli@kolli.is", UserName = "Kolbeinn" };

                usermanager.Create(user, "Kolbeinn1!");
                usermanager.AddToRole(user.Id, "admin");
            }

            if (!context.Users.Any(u => u.UserName == "Diddi"))
            {

                var user = new ApplicationUser { Email = "diddi@diddi.is", UserName = "Diddi" };

                usermanager.Create(user, "Diddi1!");
                usermanager.AddToRole(user.Id, "admin");
            }

        }
    }
}
