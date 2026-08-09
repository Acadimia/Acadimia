using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Constants
{
	public interface IConstantsService
	{
		Task<PagedResultDto<List<Constant>>> GetAllAsync(PagedResultRequestDto<Constant> input);
		Task<Constant> GetByIdOrDefaultAsync(int id);
		Task<Constant> GetByParentIdOrDefaultAsync(int id, int parentId);
        Task<OperationResult> CreateEditAsync(Constant input);
		Task<OperationResult> DeleteAsync(int id);
		Task<List<Constant>> GetParentsListItemAsync();
	}
}
