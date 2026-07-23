using Microsoft.Extensions.DependencyInjection;
//using Acadimia.Infrastructure.Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriberManagementSystem.Infrastructure.Extentions
{
    public static class ServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            //services.AddTransient<IClaimsService, ClaimsService>();
            //services.AddTransient<IClaimsService, ClaimsService>();

            return services;
        }
    }
}
