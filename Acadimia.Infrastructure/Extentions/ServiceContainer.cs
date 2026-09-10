using Acadimia.Infrastructure.Services.Constants;
using Acadimia.Infrastructure.Services.Courses;
using Acadimia.Infrastructure.Services.Lessons;
using Acadimia.Infrastructure.Services.Modules;
using Acadimia.Infrastructure.Services.Pages;
using Acadimia.Infrastructure.Services.Parent;
using Acadimia.Infrastructure.Services.Teachers;
using Acadimia.Infrastructure.Services.UserPermissions;
using Acadimia.Infrastructure.Services.Users;
using Acadimia.Infrastructure.Services.UserTypes;
using Acadimia.Infrastructure.Services.Wallets;
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


            services.AddTransient<IPagesService, PagesService>();
            services.AddTransient<IUserTypesService, UserTypesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IModulesService, ModulesService>();
            services.AddTransient<IUserPermissionsService, UserPermissionsService>();
            services.AddTransient<IConstantsService, ConstantsService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IParentService, ParentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ILessonService, LessonService>();
            //services.AddTransient<,>();
            //services.AddTransient<,>();
            //services.AddTransient<,>();


            return services;
        }
    }
}
