using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Services.Implementations.Services;
using BusinessLogic.Services.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
