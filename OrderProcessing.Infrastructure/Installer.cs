using Microsoft.Extensions.DependencyInjection;
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
	}
}
