using AutoMapper;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Acadimia.Data.Resources;
using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Acadimia.Infrastructure.Services.Users
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IMapper _mapper;

        public UsersService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base (context, userManager, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<PagedResultDto<List<UserDto>>> GetAllAsync(PagedResultRequestDto<User> input)
        {
            IQueryable<User> users = _context.Users.Include(x => x.UserType).Include(x => x.Gender)
                .Where(x => !x.IsDeleted)
                .Where(x => string.IsNullOrEmpty(input.SearchValue.Keyword)
                ? true : (x.Name.Contains(input.SearchValue.Keyword))
                || x.Email.Contains(input.SearchValue.Keyword)
                || x.PhoneNumber.Contains(input.SearchValue.Keyword)
                || x.Gender == input.SearchValue.Gender);

            if (input.SearchValue.IsActiveSearch != null)
                users = users.Where(x => x.IsActive == input.SearchValue.IsActiveSearch);

            if (input.SearchValue.UserTypeId > 0)
                users = users.Where(x => x.UserTypeId == input.SearchValue.UserTypeId);

            if (input.SearchValue.GenderId > 0)
                users = users.Where(x => x.GenderId == input.SearchValue.GenderId);

            if (!(string.IsNullOrEmpty(input.SortColumn) && string.IsNullOrEmpty(input.SortColumnDirection)))
				users = users.OrderBy(string.Concat(input.SortColumn, " ", input.SortColumnDirection));

			return new PagedResultDto<List<UserDto>>()
			{
				Data = _mapper.Map<List<UserDto>>(await users.Skip(input.Skip).Take(input.PageSize).ToListAsync()),
				TotalCount = await users.CountAsync()
			};
        }

        public async Task<UserDto> GetByIdOrDefaultAsync(string id)
        {
            var userDto = _mapper.Map<UserDto>(await _userManager.FindByIdAsync(id));
            if (userDto != null)
                return userDto;

            return new UserDto();
        }

        public async Task<List<UserType>> GetUserTypesListAsync()
        {
            return await _context.UserTypes
                .Select(c => new UserType { Id = c.Id, Name = c.Name }).ToListAsync();
        }

        public async Task<List<Constant>> GetGendersAsync()
        {
            return await _context.Constants.Where(c => c.ParentId == (int)GeneralEnums.Gender)
                .Select(c => new Constant { Id = c.Id, Name = c.Name }).ToListAsync();
        }

        public async Task<OperationResult> CreateEditAsync(UserDto input)
        {
            var result = new OperationResult();
            try
            {
                var isNewUser = string.IsNullOrEmpty(input.Id);
                var user = isNewUser ? _mapper.Map<User>(input) : await _userManager.FindByIdAsync(input.Id);

                if (user == null && !isNewUser)
                {
                    result.Message = Messages.Failed;
                    return result;
                }

				// Check if email is being changed
				bool isNameChanged = user.Name != input.Name;
				bool isAvatarChanged = user.Avatar != input.Avatar;
                if (isAvatarChanged)
                    result.OldAvatar = user.Avatar;
                
				_mapper.Map(input, user);
                
                user.UserName = input.Email;

				var currentUserId = await GetCurrentUserIdAsync();

				if (isNewUser)
                {
					user.Id = Guid.NewGuid().ToString();
					user.CreatedBy = currentUserId;
					user.CreatedOn = DateTime.Now;

					var resultManager = await _userManager.CreateAsync(user, input.Password);
                    if (!resultManager.Succeeded)
                    {
                        var errorMessages = resultManager.Errors.Select(error => error.Description).ToList();
                        result.Message = string.Join("<br>", errorMessages);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = Messages.Success;
                    }
                }
                else
                {
                    // update the user
                    if (!string.IsNullOrWhiteSpace(input.Password) && !string.IsNullOrWhiteSpace(input.ConfirmPassword))
                    {
                        var passwordUpdateResult = await UpdatePasswordAsync(user, input.Password);
                        if (!passwordUpdateResult.Success)
                        {
                            result.Message = passwordUpdateResult.Message;
                            return result;
                        }
                    }

                    user.UpdatedBy = currentUserId;
                    user.UpdatedOn = DateTime.Now;

                    var resultManager = await _userManager.UpdateAsync(user);
                    if (!resultManager.Succeeded)
                    {
                        var errorMessages = resultManager.Errors.Select(error => error.Description).ToList();
                        result.Message = string.Join("<br>", errorMessages);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = Messages.Success;

						result.IsNameChanged = isNameChanged;
						if (isNameChanged)
							result.NewName = user.Name;

						result.IsAvatarChanged = isAvatarChanged;
						if (isAvatarChanged)
							result.NewAvatar = user.Avatar;
					}
                }
            }
            catch (Exception ex)
            {
                result.Message = GetUniqueConstraintMessage(ex, new System.Collections.Generic.Dictionary<string, string>
                {
                    { "uniqueemail", Acadimia.Data.Resources.Messages.UniqueEmail },
                    { "uniquephoneno", Acadimia.Data.Resources.Messages.UniquePhoneNo }
                });
            }

            return result;
        }

        public async Task<OperationResult> DeleteAsync(string id)
        {
            var result = new OperationResult();

            // Check if there's only one user in the database
            var userCount = await _context.Users.Where(u => !u.IsDeleted).CountAsync();
            if (userCount == 1)
            {
                result.Success = false;
                result.Message = Messages.DeleteLastUser;
                return result;
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var guid = Guid.NewGuid();

                user.IsDeleted = true;
                user.DeletedBy = await GetCurrentUserIdAsync();

                user.PhoneNumber += guid;
                user.UserName += guid;
                user.NormalizedUserName += guid;
                user.Email += guid;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            return result;
        }

		public async Task<MyProfileDto> GetMyProfileAsync(string userId)
		{
			var myProfileDto = _mapper.Map<MyProfileDto>(await _userManager.FindByIdAsync(userId));
			if (myProfileDto != null)
				return myProfileDto;

			return new MyProfileDto();
		}

		public async Task<OperationResult> EditMyProfileAsync(MyProfileDto input)
		{
			var result = new OperationResult();
			try
			{
				var user = await _userManager.FindByIdAsync(input.Id);

				if (user == null)
				{
					result.Message = Messages.Failed;
					return result;
				}

                // Check if email is being changed
                bool isNameChanged = user.Name != input.Name;
                bool isAvatarChanged = user.Avatar != input.Avatar;

                _mapper.Map(input, user);

                user.UserName = input.Email;
                var resultManager = await _userManager.UpdateAsync(user);

				if (!resultManager.Succeeded)
				{
					var errorMessages = resultManager.Errors.Select(error => error.Description).ToList();
					result.Message = string.Join("<br>", errorMessages);
				}
				else
				{
                    result.Success = true;
                    result.Message = Messages.Success;

                    result.IsNameChanged = isNameChanged;
                    if (isNameChanged)
                        result.NewName = user.Name;

                    result.IsAvatarChanged = isAvatarChanged;
                    if (isAvatarChanged)
                        result.NewAvatar = user.Avatar;
                }
            }
            catch (Exception ex)
            {
                result.Message = GetUniqueConstraintMessage(ex, new System.Collections.Generic.Dictionary<string, string>
                {
                    { "uniqueemail", Acadimia.Data.Resources.Messages.UniqueEmail },
                    { "uniquephoneno", Acadimia.Data.Resources.Messages.UniquePhoneNo }
                });
            }

			return result;
		}

		public async Task<OperationResult> ChangePasswordAsync(string userId, ChangePasswordDto input)
        {
            var result = new OperationResult();
            try
            {
				var user = await _userManager.FindByIdAsync(userId);

				if (user == null)
				{
					result.Message = Messages.Failed;
					return result;
				}

				var changePasswordResult = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

				if (!changePasswordResult.Succeeded)
				{
					var errorMessages = changePasswordResult.Errors.Select(error => error.Description).ToList();
					result.Message = string.Join("<br>", errorMessages);
				}
				else
				{
					result.Success = true;
					result.Message = Messages.Success;
				}
			}
            catch (Exception)
            {
                result.Message = Messages.Failed;
			}

			return result;
		}

		private async Task<OperationResult> UpdatePasswordAsync(User user, string newPassword)
        {
            var result = new OperationResult();

            var passwordValidator = new PasswordValidator<User>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, newPassword);

            if (validationResult.Succeeded)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                result.Success = true;
            }
            else
            {
                result.Message = string.Join("<br>", validationResult.Errors.Select(error => error.Description));
            }

            return result;
        }



	}
}
