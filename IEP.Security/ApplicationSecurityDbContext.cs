using Microsoft.AspNet.Identity.EntityFramework;

namespace IEP.Security
{
    public class ApplicationSecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationSecurityDbContext() : base("IEP")
        {
        }

        public static ApplicationSecurityDbContext Create()
        {
            return new ApplicationSecurityDbContext();
        }

    }
}
