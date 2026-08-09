using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Users
{
	public interface IUsersService
	{
        Task<PagedResultDto<List<UserDto>>> GetAllAsync(PagedResultRequestDto<User> input);
        Task<UserDto> GetByIdOrDefaultAsync(string id);
        Task<OperationResult> CreateEditAsync(UserDto input);
        Task<OperationResult> DeleteAsync(string id);
        Task<List<UserType>> GetUserTypesListAsync();
        Task<List<Constant>> GetGendersAsync();
        Task<MyProfileDto> GetMyProfileAsync(string userId);
		Task<OperationResult> EditMyProfileAsync(MyProfileDto input);
        Task<OperationResult> ChangePasswordAsync(string userId, ChangePasswordDto input);
    }
}
