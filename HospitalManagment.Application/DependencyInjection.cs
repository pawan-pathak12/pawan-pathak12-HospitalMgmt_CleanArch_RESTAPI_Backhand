using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HospitalManagment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Ensure you have installed the MediatR.Extensions.Microsoft.DependencyInjection NuGet package
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
