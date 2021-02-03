using System.Linq;
using System.Reflection;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using SharedKernel.Interfaces;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), (typeof(GenericRepository<>)));

            // Services
            var assembliesToScan = new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("Core")
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
               .Where(x => x.Name.EndsWith("Service"))
               .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);


            return services;
        }
    }
}
