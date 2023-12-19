using UserManagement.IdentityDAL.Auxillary;
using UserManagement.IdentityDAL.Model;
using UserManagement.IdentityDAL.Utils;

namespace UserManagement.IdentityDAL.Service
{
    public interface IAccountService
    {
        Task<Response<bool>> RegisterAsync(ApplicationUser user, string password, string role);
        Task<Response<bool>> LoginAsync(string email, string password);
        Task<Response<bool>> LogoutAsync();
        Task<Response<bool>> DeleteUser(int id);
        Task<Response<bool>> SetNewUserStatus(ApplicationUser user,Status status, DateTime date, bool flag);

    }
}
