using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.IdentityDAL.Model
{
    public class ApplicationRole : IdentityRole<int>
    { 
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
