using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.IdentityDAL.Model
{
    public class ApplicationUser: IdentityUser<int>
    {
       
        public DateOnly RegistrationDate { get; set; }
        public DateOnly AuthorizationDate {  get; set; }
        public string Status { get; set; } = string.Empty;
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
