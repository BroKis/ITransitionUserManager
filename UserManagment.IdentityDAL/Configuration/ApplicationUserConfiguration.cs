using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.IdentityDAL.Model;

namespace UserManagement.IdentityDAL.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        void IEntityTypeConfiguration<ApplicationUser>.Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.RegistrationDate).HasColumnType("DATE");
            builder.Property(u => u.AuthorizationDate).HasColumnType("DATE");
            builder.Property(u => u.Status);
            builder.HasMany(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();

        }
    }
}
