using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.IdentityDAL.Application;

namespace UserManagement.IdentityDAL.Configuration
{
    internal class ContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            string connectionString = ConfigurationConnectionString.GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            DbContextOptions<AuthDbContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
