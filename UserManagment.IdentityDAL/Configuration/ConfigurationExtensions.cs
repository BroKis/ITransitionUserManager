using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.IdentityDAL.Application;
using UserManagement.IdentityDAL.Model;
using UserManagement.IdentityDAL.Service;

namespace UserManagement.IdentityDAL.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void AddDataAccessService(this IServiceCollection service)
        {
            service.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationConnectionString.GetConnectionString());
            }).AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789 ";
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false; opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;

            }).AddRoles<ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AuthDbContext>();
            service.AddAccountService();
            service.Configure<PasswordHasherOptions>(options =>
            options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
);

        }
        private static void AddAccountService(this IServiceCollection service)
        {
            service.AddScoped<IAccountService, AccountService>();

        }
    }
}
