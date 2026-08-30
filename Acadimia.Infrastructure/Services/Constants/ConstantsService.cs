using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Acadimia.Data.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Acadimia.Data.Models;

namespace Acadimia.Infrastructure.Services.Constants
{
	public class ConstantsService : BaseService, IConstantsService
	{
		public ConstantsService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
			: base(context, userManager, httpContextAccessor)
		{
		}

		public async Task<PagedResultDto<List<Constant>>> GetAllAsync(PagedResultRequestDto<Constant> input)
		{
			IQueryable<Constant> constants = _context.Constants.Include(c => c.Parent)
				.Where(x => string.IsNullOrEmpty(input.SearchValue.Keyword)
				? true : (x.Name.Contains(input.SearchValue.Keyword)));

			if (input.SearchValue.ParentId > 0)
				constants = constants.Where(x => x.ParentId == input.SearchValue.ParentId);

			if (!(string.IsNullOrEmpty(input.SortColumn) && string.IsNullOrEmpty(input.SortColumnDirection)))
				constants = constants.OrderBy(string.Concat(input.SortColumn, " ", input.SortColumnDirection));

			return new PagedResultDto<List<Constant>>()
			{
				Data = await constants.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
				TotalCount = await constants.CountAsync()
			};
		}

		public async Task<Constant> GetByIdOrDefaultAsync(int id)
		{
			var constant = await _context.Constants.SingleOrDefaultAsync(x => x.Id == id);
			if (constant != null)
				return constant;

			return new Constant();
		}
        public async Task<Constant> GetByParentIdOrDefaultAsync(int id, int parentId)
        {
            var constant = await _context.Constants.Where(c => c.ParentId == parentId).SingleOrDefaultAsync(x => x.Id == id);
            if (constant != null)
                return constant;

            return new Constant() { ParentId = parentId };
        }

        public async Task<OperationResult> CreateEditAsync(Constant input)
		{
			var result = new OperationResult();
			try
			{
				var currentUserId = await GetCurrentUserIdAsync();
				if (input.Id == 0)
				{
					SetCreatedFields(input, currentUserId);
					await _context.Constants.AddAsync(input);
				}
				else
				{
					SetUpdatedFields(input, currentUserId);
					SetEntityModifiedFields(input);
					_context.Constants.Update(input);
				}

				await _context.SaveChangesAsync();

				result.Success = true;
				result.Message = Messages.Success;
			}
			catch (Exception)
			{
				result.Message = Messages.Failed;
			}
			return result;
		}

		public async Task<OperationResult> DeleteAsync(int id)
		{
			var result = new OperationResult();
			var constant = await _context.Constants.SingleOrDefaultAsync(x => x.Id == id);
			if (constant != null)
			{
                var isHasChildren = await _context.Constants.AnyAsync(c => c.ParentId == constant.Id);
                if (isHasChildren)
                {
                    result.Message = Messages.ConstantHasChildren;
                    return result;
                }

                _context.Constants.Remove(constant);
				await _context.SaveChangesAsync();
				result.Success = true;
				result.Message = Messages.Success;
			}
			return result;
		}

		public async Task<List<Constant>> GetParentsListItemAsync()
		{
			return await _context.Constants.Where(c => c.ParentId == null)
				.Select(c => new Constant { Id = c.Id, Name = c.Name }).ToListAsync();
		}
	}
}
