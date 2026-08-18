using Acadimia.Infrastructure.Services.Constants;
using Acadimia.Infrastructure.Services.Modules;
using Acadimia.Infrastructure.Services.Pages;
using Acadimia.Infrastructure.Services.UserPermissions;
using Acadimia.Infrastructure.Services.Users;
using Acadimia.Infrastructure.Services.UserTypes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Extentions
{
    public static class ServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            //services.AddTransient<IClaimsService, ClaimsService>();
            //services.AddTransient<IClaimsService, ClaimsService>();
            services.AddTransient<IPagesService, PagesService>();
            services.AddTransient<IUserTypesService, UserTypesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IModulesService, ModulesService>();
            services.AddTransient<IUserPermissionsService, UserPermissionsService>();
            services.AddTransient<IConstantsService, ConstantsService>();
            //services.AddTransient<,>();
            //services.AddTransient<,>();
            //services.AddTransient<,>();


            return services;
        }
    }
}
