using FiveSys.Retaguarda.Infrastructure.CrossCutting.Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.CrossCutting.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<AppUser>().ToTable("User");
            //builder.Entity<AppRole>().ToTable("Role");
            //builder.Entity<AppUserRole>().ToTable("UserRole");

            //builder.Entity<AppUserClaim>().ToTable("UserClaim");
            //builder.Entity<AppRoleClaim>().ToTable("RoleClaim");

            //builder.Entity<AppUserLogin>().ToTable("UserLogin");
            //builder.Entity<AppUserToken>().ToTable("UserToken");
        }
    }
}
