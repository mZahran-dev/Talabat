using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Repositories;

namespace Talabat.APIS.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(Core.Repositories.Contract.GenericRepository<>), typeof(Repository.Repositories.GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>(
            options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(E => E.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();
                    var response = new ApiValidationErroResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            return services;

        }        
        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {          
            app.UseSwagger();
            app.UseSwaggerUI();      
            return app;
        }
    }
}
