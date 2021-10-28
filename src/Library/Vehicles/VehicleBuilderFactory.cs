using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsExploration.Library.Vehicles
{
    public class VehicleBuilderFactory : IVehicleBuilderFactory
    {
        private readonly IEnumerable<IVehicleBuilder> _vehicleBuilders;

        public VehicleBuilderFactory(IEnumerable<IVehicleBuilder> vehicleBuilders)
        {
            _vehicleBuilders = vehicleBuilders ?? throw new ArgumentException($"{nameof(vehicleBuilders)} should not be null");
        }

        public IVehicleBuilder Generate(VehicleType type)
        {
            var vehicleBuilder = _vehicleBuilders.FirstOrDefault(builder => builder.Type == type);

            return vehicleBuilder == null ? throw new ArgumentException($"There is not defined vehicle builder for [{type}]") : vehicleBuilder;
        }
    }
}