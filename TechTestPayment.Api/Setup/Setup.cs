using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;
using TechTestPayment.Application.Services.Concretes;
using TechTestPayment.Application.Validations;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Infrastructure.Context;
using TechTestPayment.Infrastructure.UOW;

namespace TechTestPayment.Api.Setup
{
    /// <summary>
    /// Configuration class for the application.
    /// </summary>
    /// <param name="services">Services from builder</param>
    /// <param name="configuration">Configuration from builder</param>
    /// <param name="host">Host from builder</param>
    public class Setup(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        private bool _isStopping;

        /// <summary>
        /// Configures the application before building.
        /// </summary>
        public void ConfigurePreBuild()
        {
            RegisterInjections();
            ConfigureSqlServer();
            ConfigureAutomapper();
            ConfigureLogs();
            EnrichDocumentation();
            Migrate();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the application after building.
        /// </summary>
        /// <param name="app">Web application that was just built</param>
        public void ConfigurePostBuild(WebApplication app)
        {
            app.UseExceptionHandler("/error");

            ConfigureSwaggerAppUsage(app);
            ConfigureGracefulTermination(app);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        /// <summary>
        /// Configures the application to use Swagger.
        /// </summary>
        /// <param name="app"></param>
        private static void ConfigureSwaggerAppUsage(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pottencial Tech Test");
                c.RoutePrefix = "api-docs";
            });
        }

        /// <summary>
        /// Configures the application to perform graceful termination.
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureGracefulTermination(WebApplication app)
        {
            app.Use(GracefulTermination);
            app.Lifetime.ApplicationStopping.Register(Terminate);
        }

        /// <summary>
        /// Controls IoC injections.
        /// </summary>
        private void RegisterInjections()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISellerService, SellerService>();

            services.AddScoped<Domain.Abstractions.Services.IProductService, Domain.Services.ProductService>();

            services.AddScoped<IValidator<RegisterSellerRequest>, RegisterSellerValidation>();
            services.AddScoped<IValidator<RegisterProductRequest>, RegisterProductValidation>();
            services.AddScoped<IValidator<RegisterOrderRequest>, RegisterOrderValidation>();
            services.AddScoped<IValidator<UpdateOrderRequest>, UpdateOrderValidation>();
        }

        /// <summary>
        /// Registers Automapper profiles automatically.
        /// </summary>
        private void ConfigureAutomapper()
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        private void ConfigureSqlServer()
        {
            services.AddDbContext<TechTestPaymentContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }

        /// <summary>
        /// Enriches the documentation with Swagger.
        /// </summary>
        private void EnrichDocumentation()
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pottencial Seguradora - Teste de Tecnologia",
                    Version = "v1",
                    Description = "API de simulação de vendas para apreciação técnica.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jonas Machado",
                        Email = "jonasm.1511@gmail.com"
                    }
                });

                var xmlPathApi = Path.Combine(AppContext.BaseDirectory, "TechTestPayment.Api.xml");
                var xmlPathApplication = Path.Combine(AppContext.BaseDirectory, "TechTestPayment.Application.xml");
                var xmlPathCross = Path.Combine(AppContext.BaseDirectory, "TechTestPayment.Cross.xml");
                var xmlPathDomain = Path.Combine(AppContext.BaseDirectory, "TechTestPayment.Domain.xml");

                c.IncludeXmlComments(xmlPathApi);
                c.IncludeXmlComments(xmlPathApplication);
                c.IncludeXmlComments(xmlPathCross);
                c.IncludeXmlComments(xmlPathDomain);
            });
        }

        /// <summary>
        /// Migrates the database.
        /// </summary>
        private void Migrate()
        {
            var shouldMigrate = configuration.GetValue<bool>("Migrate");

            if (shouldMigrate)
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TechTestPaymentContext>();
                context.Database.Migrate();
            }
        }

        /// <summary>
        /// Configures logs using Serilog and establishing a pattern.
        /// </summary>
        private void ConfigureLogs()
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "{Timestamp:dd/MM/yyyy HH:mm:ss} - TraceId: {RequestId} - Message: {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            host.UseSerilog();
        }

        /// <summary>
        /// Configures the application to perform graceful termination.
        /// </summary>
        /// <param name="context">Request context</param>
        /// <param name="next">To be invoked</param>
        /// <returns>Task completion or failure</returns>
        public async Task GracefulTermination(HttpContext context, Func<Task> next)
        {
            if (!_isStopping)
            {
                await next.Invoke();
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            await context.Response.WriteAsync("Server is terminating...");
            Console.WriteLine("Application is now performing graceful termination.");
        }

        /// <summary>
        /// Demands the application to handle termination gracefully.
        /// </summary>
        public void Terminate()
        {
            _isStopping = true;
        }
    }
}
