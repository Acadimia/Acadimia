using Acadimia.Api.Helper.Claims;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Auth;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Registration;
using Acadimia.Infrastructure.Services.Users;
using Acadimia.Infrastructure.Services.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Acadimia.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClaimsService _claimsService;
        private readonly ILogger<AuthController> _logger;
        private readonly IWalletService _walletService;
        private readonly IUsersService _usersService;
        private readonly IRegistrationService _registrationService;

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IClaimsService claimsService,
            ILogger<AuthController> logger,
            IWalletService walletService,
            IUsersService usersService,
            IRegistrationService registrationService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsService = claimsService;
            _logger = logger;
            _walletService = walletService;
             _usersService = usersService;
            _registrationService = registrationService;

        }

        [HttpPost]
        public async Task<OperationResult> Login(LoginDto input)
        {
            var result = new OperationResult();

            if (!ModelState.IsValid)
            {
                var message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                result.Message = message;
                return result;
            }

            var user = await _userManager.FindByNameAsync(input.Email);
            if (user != null && user.IsActive)
            {
                var resultSignIn = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);
                if (resultSignIn.Succeeded)
                {
                    await _claimsService.UpdateUserClaims(user);
                    await _signInManager.RefreshSignInAsync(user);

                    result.Success = true;
                    result.Message = Messages.Success;
                    return result;
                }
                else
                {
                    result.Message = Messages.InvalidEmailOrPasswoed;
                }
            }
            else
            {
                result.Message = Messages.InvalidEmailOrPasswoed;
            }

            return result;
        }
        [HttpPost]
        public async Task<OperationResult> Register(RegisterDto input)
        {
            if (!ModelState.IsValid)
                return new OperationResult(false, string.Join("<br>",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

            var (result, user) = await _registrationService.RegisterAsync(input);
            if (!result.Success || user == null) return result;

            await _claimsService.UpdateUserClaims(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationOptions() => Ok(await _usersService.GetRegistrationOptionsAsync());

        [HttpPost]
        public async Task<OperationResult> Logout()
        {
            var result = new OperationResult();

            await _signInManager.SignOutAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }
    }
}