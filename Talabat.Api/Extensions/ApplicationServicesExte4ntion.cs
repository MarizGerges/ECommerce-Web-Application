using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Errors;
using Talabat.Api.Helpers;
using Talabat.Core;
using Talabat.Core.Repositries;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.Api.Extensions
{
    public static class ApplicationServicesExte4ntion
    {
        public static IServiceCollection AddAplicationServices (this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepositry),typeof (BasketRepositry));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrederService>();
            services.AddScoped<IPaymentService, PaymentService>();


            // services.AddScoped(typeof(IGenaricRepositery<>), typeof(GenaricRepositery<>));

            //services.AddAutoMapper(M => M.AddProfile(new MappingProfile())); we can use this or the next one

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToArray();



                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };

            });

            return services;

        }
    }
}
