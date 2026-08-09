using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.UserTypes
{
	public interface IUserTypesService
	{
		Task<PagedResultDto<List<UserType>>> GetAllAsync(PagedResultRequestDto<UserType> input);
		Task<UserType> GetByIdOrDefaultAsync(int id);
		Task<OperationResult> CreateEditAsync(UserType input);
		Task<OperationResult> DeleteAsync(int id);
	}
}
