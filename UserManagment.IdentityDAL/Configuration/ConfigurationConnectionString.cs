using Microsoft.Extensions.Configuration;

namespace UserManagement.IdentityDAL.Configuration
{
    public class ConfigurationConnectionString
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
