using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using Talabat.Api.Errors;
using Talabat.Api.Extensions;
using Talabat.Api.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositries;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllers(); // allow debandency injection for api services

            builder.Services.AddSwaggerServices();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return  ConnectionMultiplexer.Connect(connection);
            });


            builder.Services.AddAplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            #endregion


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services= scope.ServiceProvider;

            var loggerFactory= services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); //ask explicitly
                await dbContext.Database.MigrateAsync(); // apply Migrations
                await StoreContextSeed.SeedAsync(dbContext);


                var identityContext= services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                 
                var userManger = services.GetRequiredService <UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManger);


            }
            catch (Exception ex)
            {
                var logger=loggerFactory.CreateLogger<Program>();
                logger.LogError(ex , ex.Message);
            }

            // Configure the HTTP request pipeline.
            #region Configure Kestrele Middlewares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlwares();
            }
            app.UseStatusCodePagesWithRedirects ("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}