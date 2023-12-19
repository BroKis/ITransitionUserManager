using Microsoft.AspNetCore.Identity;

namespace UserManagement.IdentityDAL.Model
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
