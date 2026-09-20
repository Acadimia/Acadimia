using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Ownership;
using Acadimia.Infrastructure.Services.UserPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Acadimia.Api.Helper.Authorization
{
    /// يعفي action من فحص صلاحية الصفحة (خدمات ذاتية: ملفي الشخصي، تغيير كلمة المرور)
    [AttributeUsage(AttributeTargets.Method)]
    public class SkipPagePermissionAttribute : Attribute { }

    /// يفحص UserPermissions: هل نوع المستخدم عنده صفحة Link == "Controller/Action"
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirePagePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext ctx)
        {
            var meta = ctx.ActionDescriptor.EndpointMetadata;
            if (meta.OfType<IAllowAnonymous>().Any() || meta.OfType<SkipPagePermissionAttribute>().Any())
                return;

            var sp = ctx.HttpContext.RequestServices;
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var user = await userManager.GetUserAsync(ctx.HttpContext.User);
            if (user == null || user.IsDeleted || !user.IsActive)
            {
                ctx.Result = new UnauthorizedResult();
                return;
            }

            var controller = ctx.RouteData.Values["controller"]?.ToString();
            var action = ctx.RouteData.Values["action"]?.ToString();
            var url = $"{controller}/{action}".ToLowerInvariant();

            var permissions = sp.GetRequiredService<IUserPermissionsService>();
            if (!await permissions.HasPermissionAsync(user, url))
                ctx.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }

    /// يسمح فقط لأنواع مستخدمين محددة (للـ endpoints يلي ما إلها صفحة بجدول Pages)
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequireUserTypesAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly int[] _allowed;
        public RequireUserTypesAttribute(params int[] allowedUserTypeIds) => _allowed = allowedUserTypeIds;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext ctx)
        {
            if (ctx.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any()) return;

            var userId = ctx.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { ctx.Result = new UnauthorizedResult(); return; }

            var ownership = ctx.HttpContext.RequestServices.GetRequiredService<IOwnershipService>();
            var typeId = await ownership.GetUserTypeIdAsync(userId);
            if (typeId == null || !_allowed.Contains(typeId.Value))
                ctx.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}