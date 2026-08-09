using Acadimia.Infrastructure.Services.Constants;
using Acadimia.Infrastructure.Services.Pages;
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

            return services;
        }
    }
}
