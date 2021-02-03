using System.Linq;
using System.Reflection;
using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
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

            // ValidationError
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });


            return services;
        }
    }
}
