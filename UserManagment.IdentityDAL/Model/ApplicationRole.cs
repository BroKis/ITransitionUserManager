using Microsoft.AspNetCore.Identity;

namespace UserManagement.IdentityDAL.Model
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
