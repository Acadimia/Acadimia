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

		protected void SetCreatedFields(object entity, string userId)
		{
			if (entity is BaseModel bm)
			{
				bm.CreatedBy = userId;
				bm.CreatedOn = DateTime.Now;
			}
		}

		protected void SetUpdatedFields(object entity, string userId)
		{
			if (entity is BaseModel bm)
			{
				bm.UpdatedBy = userId;
				bm.UpdatedOn = DateTime.Now;
			}
		}

		protected void SetEntityModifiedFields(object entity)
		{
			if (entity is BaseModel bm)
			{
				_context.Entry(bm).Property(x => x.CreatedOn).IsModified = false;
				_context.Entry(bm).Property(x => x.CreatedBy).IsModified = false;
			}
		}

		/// <summary>
		/// Parses database unique-constraint exception inner message and maps it to a friendly message
		/// mapping is a dictionary where key is the constraint token (e.g. "uniqueemail") and value is the friendly message.
		/// </summary>
		protected string GetUniqueConstraintMessage(Exception ex, System.Collections.Generic.Dictionary<string, string> mapping)
		{
			if (ex?.InnerException?.Message == null)
				return Acadimia.Data.Resources.Messages.Failed;

			var message = ex.InnerException.Message;
			string execptionType;
			try
			{
				var parts = message.Split("_");
				execptionType = parts[parts.Length - 1].Split('\'')[0].ToLower();
			}
			catch
			{
				return Acadimia.Data.Resources.Messages.Failed;
			}

			if (mapping != null && mapping.TryGetValue(execptionType, out var mapped))
				return mapped;

			return Acadimia.Data.Resources.Messages.Failed;
		}
    }
}
