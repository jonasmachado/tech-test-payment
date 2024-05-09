using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;
using TechTestPayment.Application.Services.Concretes;
using TechTestPayment.Application.Validations;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Infrastructure.Context;
using TechTestPayment.Infrastructure.UOW;

namespace TechTestPayment.Tests.Integration.Setup
{
    public class IntegrationTestsSetup
    {
        public static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISellerService, SellerService>();

            services.AddScoped<SellersController>();
            services.AddScoped<ProductsController>();
            services.AddScoped<OrdersController>();

            services.AddScoped<Domain.Abstractions.Services.IProductService, Domain.Services.ProductService>();

            services.AddScoped<IValidator<RegisterSellerRequest>, RegisterSellerValidation>();
            services.AddScoped<IValidator<RegisterProductRequest>, RegisterProductValidation>();
            services.AddScoped<IValidator<RegisterOrderRequest>, RegisterOrderValidation>();
            services.AddScoped<IValidator<UpdateOrderRequest>, UpdateOrderValidation>();

            services.AddDbContext<TechTestPaymentContext>(options =>
                options.UseInMemoryDatabase("tech-test-payment")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped(typeof(ILogger<OrderService>), _ =>
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                });

                return loggerFactory.CreateLogger<OrderService>();
            });

            services.AddScoped(typeof(ILogger<Domain.Services.ProductService>), _ =>
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                });

                return loggerFactory.CreateLogger<Domain.Services.ProductService>();
            });

            return services.BuildServiceProvider();
        }
    }
}
