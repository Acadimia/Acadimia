using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services
{
	public abstract class BaseService
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<User> _userManager;
		protected readonly IHttpContextAccessor _httpContextAccessor;

		public BaseService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		protected async Task<string> GetCurrentUserIdAsync()
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			return user?.Id;
		}

        protected async Task<string> GetCurrentUserNameAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user?.Name;
        }

        protected void SetCreatedFields(BaseModel entity, string userId)
		{
			entity.CreatedBy = userId;
			entity.CreatedOn = DateTime.Now;
		}

		protected void SetUpdatedFields(BaseModel entity, string userId)
		{
			entity.UpdatedBy = userId;
			entity.UpdatedOn = DateTime.Now;
		}

        protected void SetEntityModifiedFields(BaseModel entity)
        {
            _context.Entry(entity).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        }
    }
}
