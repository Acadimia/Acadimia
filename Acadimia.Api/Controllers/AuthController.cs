using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services;
using Acadimia.Web.Helper.Claims;
using Acadimia.Web.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Timers;


namespace Acadimia.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClaimsService _claimsService;
        private readonly ILogger<AuthController> _logger;


        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IClaimsService claimsService,
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsService = claimsService;
            _logger = logger;
        }


        [HttpPost("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<OperationResult> Login(LoginDTO input)
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
                    await _signInManager.RefreshSignInAsync(user); // Refresh the authentication session

                    result.Success = true;
                    if (!string.IsNullOrEmpty(input.ReturnUrl) && Url.IsLocalUrl(input.ReturnUrl))
                    {
                        if (input.ReturnUrl == "/")
                            input.ReturnUrl = "/Home";

                        result.Message = input.ReturnUrl; // Using Message property to pass the URL for redirection
                    }
                    else
                    {
                        result.Message = "/Home"; // Provide a default redirection URL
                    }

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
        public async Task<OperationResult> Login2(LoginDTO input)
        {
            var result = new OperationResult();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000; // Set your threshold time in milliseconds (e.g., 10 seconds)
            bool isTimeout = false;

            // Event to handle what happens when the timer elapses
            timer.Elapsed += (sender, e) =>
            {
                isTimeout = true;
                timer.Stop(); // Stop the timer after it elapses
                              // Log or handle timeout here
                Console.WriteLine("Login action is taking too long.");
            };

            timer.Start(); // Start the timer
            Stopwatch stopwatch = Stopwatch.StartNew(); // Start the stopwatch

            try
            {
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
                    stopwatch.Restart(); // Restart the stopwatch to measure the next block
                    var resultSignIn = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);
                    Console.WriteLine($"Time taken for PasswordSignInAsync: {stopwatch.Elapsed.TotalSeconds} seconds");

                    if (resultSignIn.Succeeded)
                    {
                        stopwatch.Restart(); // Restart the stopwatch to measure the next block
                        await _claimsService.UpdateUserClaims(user);
                        await _signInManager.RefreshSignInAsync(user); // Refresh the authentication session
                        Console.WriteLine($"Time taken for UpdateUserClaims and RefreshSignInAsync: {stopwatch.Elapsed.TotalSeconds} seconds");

                        result.Success = true;
                        if (!string.IsNullOrEmpty(input.ReturnUrl) && Url.IsLocalUrl(input.ReturnUrl))
                        {
                            if (input.ReturnUrl == "/")
                                input.ReturnUrl = "/Home";

                            result.Message = input.ReturnUrl; // Using Message property to pass the URL for redirection
                            Console.WriteLine($"Time taken for setting return URL: {stopwatch.Elapsed.TotalSeconds} seconds");
                        }
                        else
                        {
                            result.Message = "/Home"; // Provide a default redirection URL
                            Console.WriteLine($"Time taken for default return URL: {stopwatch.Elapsed.TotalSeconds} seconds");
                        }

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
            finally
            {
                timer.Stop(); // Ensure the timer stops when the action completes
                if (!isTimeout)
                {
                    // Action completed within the acceptable time frame
                    Console.WriteLine("Login action completed successfully within the time limit.");
                }
                stopwatch.Stop(); // Stop the stopwatch when done
            }
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult EmailChanged() => View();

        [HttpGet]
        public IActionResult NotFound() => View();

    }
}
