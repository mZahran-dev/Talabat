
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIS.Errors;
using Talabat.APIS.Extensions;
using Talabat.APIS.Helpers;
using Talabat.APIS.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;
using Talabat.Repository.Repositories;

namespace Talabat.APIS
{
    public  class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add dependency injection services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<StoreContext>
            (
             options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddDbContext<AppIdentityDbContext>
            (
             options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
            );
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            //ApplicationServicesExtensions.AddApplicationServices(builder.Services);
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");    
                return ConnectionMultiplexer.Connect(connection);
            }
            );
            builder.Services.AddApplicationServices(); //Extension Method
            var app = builder.Build();

            #endregion

            // Update DataBase Automatically
            #region Update Database 
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during Migration");
            }
            #endregion

            #region Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }
            //only one request on network => better to front ends
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            // two requests on network
            app.UseStatusCodePagesWithRedirects("/Errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
