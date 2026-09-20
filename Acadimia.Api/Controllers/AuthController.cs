using Acadimia.Api.Helper.Claims;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Auth;
using Acadimia.Infrastructure.Services;
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
        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IClaimsService claimsService,
            ILogger<AuthController> logger,
            IWalletService walletService,
            IUsersService usersService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsService = claimsService;
            _logger = logger;
            _walletService = walletService;
             _usersService = usersService;

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
            var result = new OperationResult();

            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            var existingByEmail = await _userManager.FindByNameAsync(input.Email);
            if (existingByEmail != null)
            {
                result.Message = Messages.UniqueEmail;
                return result;
            }
            var userTypeId = input.UserTypeId ?? UserTypeIds.Student;

            if (!UserTypeIds.SelfRegistration.Contains(userTypeId))
            {
                result.Message = Messages.Invalid;
                return result;
            }
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Email = input.Email,
                UserName = input.Email,
                PhoneNumber = input.PhoneNumber,
                GenderId = input.GenderId,
                UserTypeId = input.UserTypeId ?? 2,
                IsActive = true,
                Avatar = "default_avatar.png",
                CreatedOn = DateTime.Now
            };

            var createResult = await _userManager.CreateAsync(user, input.Password);

            if (!createResult.Succeeded)
            {
                var errorMessages = createResult.Errors.Select(e => e.Description).ToList();
                result.Message = string.Join("<br>", errorMessages);
                return result;
            }

            // إنشاء Wallet للمستخدم الجديد عبر WalletService
            var walletResult = await _walletService.CreateWalletForUserAsync(user.Id);
            if (!walletResult.Success)
            {
                _logger.LogError("Failed to create wallet for user {UserId}: {Message}", user.Id, walletResult.Message);
            }

            await _claimsService.UpdateUserClaims(user);
            await _signInManager.SignInAsync(user, isPersistent: false);

            result.Success = true;
            result.Message = Messages.Success;
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