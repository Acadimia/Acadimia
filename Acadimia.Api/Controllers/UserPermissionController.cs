using Acadimia.Data;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.UserPermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Api.Controllers
{
    public class UserPermissionController : BaseController
    {
        private readonly IUserPermissionsService _userPermissionsService;

        public UserPermissionController(IUserPermissionsService userPermissionsService)
        {
            _userPermissionsService = userPermissionsService;
        }

        // display User Type Permissions
        [HttpPost]
        public async Task<IActionResult> GetUserTypePermissions(int userTypeId)
        {
            var permissions = await _userPermissionsService.GetUserTypePermissionsAsync(userTypeId);
            return Ok(permissions);   // <-- fixed
        }

        // Save User Type Permissions
        [HttpPost]
        public async Task<OperationResult> SavePermissions(int userTypeId, List<UserPermission> permissions)
        {
            return await _userPermissionsService.SavePermissionsAsync(userTypeId, permissions);
        }
    }
}