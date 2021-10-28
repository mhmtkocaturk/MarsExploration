using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Registry;
using MarsExploration.Library.Controllers;
using MarsExploration.Library.Vehicles;
using Microsoft.Extensions.DependencyInjection;

namespace MarsExploration.Infrastructure.Registry
{
    public class VehicleRegistry : RegistryBase
    {
        public VehicleRegistry()
        {
            Register(typeof(IVehicleBuilder), typeof(RoverBuilder), ServiceLifetime.Singleton);
            Register(typeof(IVehicleBuilderFactory), typeof(VehicleBuilderFactory), ServiceLifetime.Singleton);
            Register(typeof(IMarsController), typeof(MarsController), ServiceLifetime.Singleton);
        }
    }
}