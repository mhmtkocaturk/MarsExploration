using MarsExploration.Common.Registry;
using MarsExploration.Infrastructure.Registry;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsExploration.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Add<T>(this IServiceCollection services) where T : RegistryBase, new()
        {
            var registry = Activator.CreateInstance<T>();
            var descriptors = registry.GetDescriptors();
            foreach (var desc in descriptors)
            {
                services.Add(desc);
            }
            return services;
        }

        public static IServiceCollection AddMarsExplorationServices(this IServiceCollection services)
        {
            services.Add<SurfaceRegistry>();
            services.Add<VehicleRegistry>();

            return services;
        }
    }
}