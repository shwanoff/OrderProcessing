using FluentValidation.AspNetCore;
using FluentValidation;
using MartinCostello.OpenApi;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application.Handlers.Commands;
using OrderProcessing.Infrastructure;
using Serilog;
using System.Reflection;
using OrderProcessing.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace OrderProcessing.Infrastructure
{
	public static class Helpers
	{
		public static void AddInMemoryDatabase(this IServiceCollection services)
		{
			services.AddDbContext<OrderDbContext>(options =>
					options.UseInMemoryDatabase("InMemoryDbForTesting"));
		}

		public static void AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<OrderDbContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		}

		public static void AddSerilogService(this IHostBuilder hostBuilder, IConfiguration configuration)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();

			hostBuilder.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services));
		}

		public static void AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
		}

		public static void AddMediatR(this IServiceCollection services)
		{
			services.AddMediatRServices("OrderProcessing.Application");
		}

		public static IServiceCollection AddMediatRServices(this IServiceCollection services, params string[] assemblyNames)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddMediatR(cfg =>
			{
				foreach (var assemblyName in assemblyNames)
					cfg.RegisterServicesFromAssembly(Assembly.Load(assemblyName));
			});

			return services;
		}

		public static void AddFluentValidation(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();
			services.AddFluentValidationAutoValidation();
		}

		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		public static void AddCustomOpenApi(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddOpenApi();

			// TODO: Fix issue with OpenAPI generation
			// There is a well-known issue with the OpenAPI generation
			// https://github.com/dotnet/aspnetcore/issues/57332#issuecomment-2479286855
			// To fix this issue, we need to add the following line of code from personal library
			// https://github.com/martincostello/openapi-extensions?tab=readme-ov-file
			services.AddOpenApiExtensions(options => options.AddServerUrls = true);
			services.AddHttpContextAccessor();
			// end of fix
		}

		public static void MigrateDatabase(this IServiceProvider serviceProvider)
		{
			// TODO: It is not recommended to apply migrations in the application startup
			// It is better to apply migrations manually or use specific pipeline for that
			// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
			// but for demo purposes, we will apply migrations in the application startup
			using var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
			dbContext.Database.Migrate();
		}

		public static void UseCustomSwaggerUI(this IApplicationBuilder app)
		{
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/openapi/v1.json", "OrderProcessing.API v1");
				options.RoutePrefix = string.Empty;
			});
		}
	}
}
