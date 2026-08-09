using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.UserPermissions;
using Acadimia.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Acadimia.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        //public override async void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);
        //}
    }
}
