using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Client.Models;
using UserManagement.IdentityDAL.Model;
using UserManagement.IdentityDAL.Service;

namespace UserManagement.Client.Controllers
{
    public class AccountController : Controller
    {
        private List<UserForDisplay> _users = new List<UserForDisplay>();
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, IMapper mapper, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _users = _mapper.Map<List<UserForDisplay>>(_userManager.Users);
            return View(_users);
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountService.LogoutAsync();

            if (result.StatusCode != IdentityDAL.Auxillary.StatusCode.OK)
            {
                View("Error",
                  new ErrorViewModel
                  {
                      Controller = "Account",
                      Description = result.Description
                  });
            }

            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationForm(UserForSignUp model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);
            var result = await _accountService.RegisterAsync(user, model.Password, model.Role);
            if (result.StatusCode == IdentityDAL.Auxillary.StatusCode.OK)
            {
                return RedirectToAction("Index", "Account");
            }
            ModelState.AddModelError(string.Empty, result.Description);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginForm(UserForSignIn model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.LoginAsync(model.Email, model.Password);
            if (result.StatusCode == IdentityDAL.Auxillary.StatusCode.OK)
            {
                return RedirectToAction("Index", "Account");

            }
            ModelState.AddModelError(string.Empty, result.Description);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Lock(string [] selectedIds)
        {
            if (ModelState.IsValid)
            {
                foreach (string id in selectedIds)
                {
                    ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    var result = await _accountService.BlockUser(user);
                    _logger.LogInformation(result.Description);
                }
                return RedirectToAction("Index", "Account");
            }
           return View("Error", new ErrorViewModel
           {
                Controller = "Account",
                Description = "You should to pick users"
           });
        }
        [HttpPost]
        public async Task<IActionResult> Unlock(string[] selectedIds)
        {
            if (ModelState.IsValid)
            {
                foreach (string id in selectedIds)
                {
                    ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    var result = await _accountService.UnblockUser(user);
                    _logger.LogInformation(result.Description);
                }
                return RedirectToAction("Index", "Account");
            }
            return View("Error", new ErrorViewModel
            {
                Controller = "Account",
                Description = "You should to pick users"
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedIds)
        {
            if (ModelState.IsValid)
            {
                foreach (string id in selectedIds)
                {
                    var result = await _accountService.DeleteUser(Convert.ToInt32(id));
                    _logger.LogInformation(result.Description);
                }
                return RedirectToAction("Index", "Account");
            }
            return View("Error", new ErrorViewModel
            {
                Controller = "Account",
                Description = "You should to pick users"
            });
        }
    }
}
