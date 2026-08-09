using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services.UserPermissions.Dto;
using Acadimia.Data.DbContext;

namespace Acadimia.Infrastructure.Services.UserPermissions
{
    public class UserPermissionsService : IUserPermissionsService
    {
        private readonly ApplicationDbContext _context;
        public UserPermissionsService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<UserPermissionsDto> GetUserPermissionsAsync()
        {
			var userTypes = await _context.UserTypes.ToListAsync();
			var pages = await _context.Pages.Include(p => p.Category).Include(p => p.Parent)
				.Where(x => x.Id != 1) // without parent page.  
				.ToListAsync();

			return new UserPermissionsDto
			{
				UserTypes = userTypes,
				Pages = pages
			};
		}

		public async Task<List<int>> GetUserTypePermissionsAsync(int userTypeId)
		{
			var permissions = await _context.UserPermissions
				.Where(up => up.PageId != 1 && up.UserTypeId == userTypeId) // and without parent page.  
				.Select(up => up.PageId).ToListAsync();

			return permissions;
		}

        public async Task<OperationResult> SavePermissionsAsync(int userTypeId, List<UserPermission> permissions)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var userType = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == userTypeId);
            if (userType != null)
            {
                var existingPermissions = await _context.UserPermissions.Where(up => up.UserTypeId == userTypeId).ToListAsync();

                // Find permissions to be added
                var permissionsToAdd = permissions
                    .Where(np => !existingPermissions.Any(ep => ep.UserTypeId == userTypeId && ep.PageId == np.PageId))
                    .ToList();

                // Find permissions to be removed
                var permissionsToRemove = existingPermissions
                    .Where(ep => !permissions.Any(np => np.UserTypeId == userTypeId && np.PageId == ep.PageId))
                    .ToList();

                // Remove old permissions
                _context.UserPermissions.RemoveRange(permissionsToRemove);

                // Add new permissions
                foreach (var permission in permissionsToAdd)
                {
                    permission.UserTypeId = userTypeId;
                    _context.UserPermissions.Add(permission);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }

            return result;
        }

        public async Task<bool> HasPermissionAsync(User user, string pageUrl)
        {
            var hasPermission = await _context.UserPermissions.AsNoTracking()
                .AnyAsync(up => up.UserTypeId == user.UserTypeId
                    && up.Page.Link.ToLower() == pageUrl
                    && (up.Page.Module == null || up.Page.Module.Status));

            return hasPermission;
        }


        public async Task<bool> PageExistsAsync(string pageUrl)
        {
            var pages = await _context.Pages.AsNoTracking().Where(p => p.Link != null)
                                      .ToListAsync();

            var flag = pages.Any(p => p.Link.Trim('/').ToLower() == pageUrl);
            return flag;
        }

    }
}
