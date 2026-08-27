using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services.Users;
using Acadimia.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Acadimia.Infrastructure.Services.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Acadimia.Api.Helper.Claims;
using Acadimia.Api.Helper.Files;
using Newtonsoft.Json;
using Acadimia.Infrastructure.Dtos.User;

namespace Acadimia.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClaimsService _claimsService;
        private readonly IFileService _fileService;

        public UserController(
            IUsersService usersService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IClaimsService claimsService,
            IFileService fileService)
        {
            _usersService = usersService;
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsService = claimsService;
            _fileService = fileService;
        }

        [HttpPost] // display User DataTable
        public async Task<IActionResult> GetAll([FromBody] UserDataTableRequestDto? request = null)
        {
            request ??= new UserDataTableRequestDto();

            var filter = new User
            {
                Keyword = request.SearchValue,
                UserTypeId = request.UserTypeId ?? 0,
                GenderId = request.GenderId,
                IsActiveSearch = request.IsActiveSearch
            };

            var result = await _usersService.GetAllAsync(new PagedResultRequestDto<User>
            {
                SearchValue = filter,
                SortColumn = request.SortColumn,
                SortColumnDirection = request.SortColumnDirection,
                PageSize = request.PageSize,
                Skip = request.Skip
            });

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }


        [HttpGet] // returns data for create/edit User form
        public async Task<IActionResult> CreateEditModal(string id)
        {
            return Ok(new CreateEditUser
            {
                User = await _usersService.GetByIdOrDefaultAsync(id),
                UserTypes = await _usersService.GetUserTypesListAsync(),
                Genders = await _usersService.GetGendersAsync()
            });
        }

        [HttpPost] // create Edit User 
        public async Task<OperationResult> CreateEdit(UserDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!string.IsNullOrEmpty(input.Id))
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
            }

            if (!ModelState.IsValid)
            {
                var message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                result.Message = message;
                return result;
            }

            var resultCreatEditUser = await _usersService.CreateEditAsync(input);

            if (resultCreatEditUser.Success)
            {
                if (resultCreatEditUser.IsAvatarChanged && !resultCreatEditUser.OldAvatar.Equals("default_avatar.png"))
                    await _fileService.DeleteFile("Images", resultCreatEditUser.OldAvatar);

                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (loggedInUserId == input.Id)
                    await UpdateClaimsIfNecessary(resultCreatEditUser, input.Id);
            }
            
            return resultCreatEditUser;
		}

        [HttpDelete] // Delete User
        public async Task<OperationResult> Delete(string id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == loggedInUserId)
                return new OperationResult(false, Messages.FailedDeleteLoggedAccount);

            return await _usersService.DeleteAsync(id);
        }

        [HttpGet]  // returns current user's profile data
        public async Task<IActionResult> MyProfileModal()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myProfileDto = await _usersService.GetMyProfileAsync(loggedInUserId);

            return Ok(new MyProfile()
            {
                MyProfileDto = myProfileDto,
                Genders = await _usersService.GetGendersAsync()
            });
        }

        [HttpPost] // Edit my profile User
        public async Task<OperationResult> MyProfile(MyProfileDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
                       
            if (!ModelState.IsValid)
            {
                var message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                result.Message = message;
                return result;
            }

            var resultEditMyProfile = await _usersService.EditMyProfileAsync(input);

            if (resultEditMyProfile.Success)
            {
                await UpdateClaimsIfNecessary(resultEditMyProfile, input.Id);
			}

            return resultEditMyProfile;
        }

        [HttpGet] 
        public IActionResult ChangePasswordModal()
        {
            return Ok(new ChangePasswordDto());
        }

        [HttpPost] // Change Password
        public async Task<OperationResult> ChangePassword(ChangePasswordDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                var message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                result.Message = message;
                return result;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await _usersService.ChangePasswordAsync(userId, input);
        }

		private async Task UpdateClaimsIfNecessary(OperationResult operationResult, string userId)
		{
			if (operationResult.IsNameChanged || operationResult.IsAvatarChanged)
			{
				var user = await _userManager.FindByIdAsync(userId);
				if (user != null)
				{
					await _claimsService.UpdateUserClaims(user);
					await _signInManager.RefreshSignInAsync(user);
				}
			}
		}

	}
}
