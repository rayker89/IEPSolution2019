namespace IEP.Security.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IEP.Security.ApplicationSecurityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IEP.Security.ApplicationSecurityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any())
            {
                var store = new RoleStore<ApplicationRole, int, ApplicationUserRole>(context);
                var manager = new ApplicationRoleManager(store);

                var role = new ApplicationRole { Name = "Admin" };
                manager.Create(role);

                role = new ApplicationRole { Name = "Registred User" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
                var manager = new ApplicationUserManager(store);
                var user = new ApplicationUser { UserName = "Admin", Email = "admin@iep.rs", FirstName = "Admin", LastName = "Admin" };

                manager.Create(user, "password");
                manager.AddToRole(user.Id, "Admin");
            }

        }
    }
}
