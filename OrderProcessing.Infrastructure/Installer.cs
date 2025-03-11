using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Application.Interfaces;
using System.Reflection;

namespace OrderProcessing.Infrastructure
{
    public static class Installer
    {
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

		public static IServiceCollection AddRepository(this IServiceCollection services)
		{
			services.AddScoped<IOrderRepository, OrderRepository>();
			return services;
		}
	}
}
