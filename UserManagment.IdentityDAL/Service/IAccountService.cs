using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.IdentityDAL.Model;
using UserManagement.IdentityDAL.Utils;

namespace UserManagement.IdentityDAL.Service
{
    public interface IAccountService
    {
        Task<Response<bool>> RegisterAsync(ApplicationUser user, string password,string role);
        Task<Response<bool>> LoginAsync(string email, string password);
        Task<Response<bool>> LogoutAsync();
        Task<Response<bool>> BlockUser(ApplicationUser user);
        Task<Response<bool>> UnblockUser(ApplicationUser user);
        Task<Response<bool>> DeleteUser(int id);
       
    }
}
