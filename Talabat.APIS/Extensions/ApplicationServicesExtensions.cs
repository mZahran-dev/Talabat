﻿using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.UnitOfWork.Contract;
using Talabat.Repository.Repositories;
using Talabat.Repository.UnitOfWork;
using Talabat.Service;

namespace Talabat.APIS.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
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
