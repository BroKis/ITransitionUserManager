﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using UserManagement.IdentityDAL.Auxillary;
using UserManagement.IdentityDAL.Model;
using UserManagement.IdentityDAL.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace UserManagement.IdentityDAL.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<Response<bool>> BlockUser(ApplicationUser user)
        {
            var response = new Response<bool>();
            try
            {
                await UpdateStatus(user, Status.Blocked);
                await UpdateLockTime(user,true, new DateTime(9999, 01, 01));
                return response.GetResponse(true, StatusCode.OK, $"User {user.UserName} was blocked");
            }
            catch (Exception ex)
            {
                return response.GetResponse(false, StatusCode.BadRequest, ex.Message);
            }
        }

        private async Task UpdateLockTime(ApplicationUser user,bool flag, DateTime endDate)
        {
            await _userManager.SetLockoutEnabledAsync(user, flag);
            await _userManager.SetLockoutEndDateAsync(user, endDate);
        }

        private async Task UpdateStatus(ApplicationUser user, Status status)
        {
            user.Status = status.ToString();
            await _userManager.UpdateAsync(user);
        }

        public async Task<Response<bool>> DeleteUser(int id)
        {
            var response = new Response<bool>();
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            try
            {
                if (user is null)
                {
                    return response.GetResponse(false, StatusCode.BadRequest, "Data can't be null");
                }
                var result = await _userManager.DeleteAsync(user);
                return response.GetResponse(true, StatusCode.OK, $"The user {user.UserName} was deleted successfully");
                
            }catch(Exception ex)
            {
                return response.GetResponse(false, StatusCode.BadRequest, ex.Message);
            }
            
        }

        public async Task<Response<bool>> LoginAsync(string email, string password)
        {
            var response = new Response<bool>();
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (!result.Succeeded)
                    return response.GetResponse(false, StatusCode.NotFound, "Invalid email or password");

                user.AuthorizationDate = DateOnly.FromDateTime(DateTime.Now);
                await _userManager.UpdateAsync(user);
                return response.GetResponse(true, StatusCode.OK, "The user was sign in successfully");
            }
            catch (Exception ex)
            {
                return response.GetResponse(false, StatusCode.OK, ex.Message);
            }
        }

        public async Task<Response<bool>> LogoutAsync()
        {
            var response = new Response<bool>();

            try
            {
                await _signInManager.SignOutAsync();
                return response.GetResponse(true, StatusCode.OK, "The user was sign in successfully");
            }
            catch (Exception ex)
            {
                return response.GetResponse(false,StatusCode.BadRequest,ex.Message);
            }
        }

        public async Task<Response<bool>> RegisterAsync(ApplicationUser user, string password, string role)
        {
            var response = new Response<bool>();
            try
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new ApplicationRole() { Name = role,NormalizedName = role.ToUpper()});
                }
                if (user is null)
                {
                    return response.GetResponse(false, StatusCode.BadRequest, "Data can't be null");
                }
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var errors = new StringBuilder(16);
                    result.Errors.ToList().ForEach(i => errors.Append(i.Description));
                    return response.GetResponse(false,StatusCode.BadRequest,errors.ToString());
                }
                await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, false);
                return response.GetResponse(true,StatusCode.OK, "The user was created successfully");
            }
            catch (Exception ex)
            {
                return response.GetResponse(false, StatusCode.OK, ex.Message);
            }
        }

        public async Task<Response<bool>> UnblockUser(ApplicationUser user)
        {
            var response = new Response<bool>();
            try
            {
                await UpdateStatus(user, Status.Active);
                await UpdateLockTime(user, true, DateTime.Now - TimeSpan.FromMinutes(1));
                return response.GetResponse(true, StatusCode.OK, $"User {user.UserName} was unblocked");
            }
            catch(Exception ex)
            {
                return response.GetResponse(false, StatusCode.OK, ex.Message);
            }
        }
       

    }
}
