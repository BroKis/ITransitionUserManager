using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.IdentityDAL.Configuration;
using UserManagement.IdentityDAL.Model;

namespace UserManagement.IdentityDAL.Application
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {


        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationConnectionString.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (Database.IsRelational())
            {
                modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
                modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());

            }
        }
    }
}
