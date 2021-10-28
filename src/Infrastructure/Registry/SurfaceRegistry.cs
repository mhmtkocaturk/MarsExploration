using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Registry;
using MarsExploration.Library.Surfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MarsExploration.Infrastructure.Registry
{
    public class SurfaceRegistry : RegistryBase
    {
        public SurfaceRegistry()
        {
            Register(typeof(ISurfaceBuilder), typeof(PlateauBuilder), ServiceLifetime.Singleton);
            Register(typeof(ISurfaceBuilderFactory), typeof(SurfaceBuilderFactory), ServiceLifetime.Singleton);
        }
    }
}