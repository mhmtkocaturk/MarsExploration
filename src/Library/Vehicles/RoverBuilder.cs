using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;
using MarsExploration.Model.Vehicles;

namespace MarsExploration.Library.Vehicles
{
    public class RoverBuilder : IVehicleBuilder
    {
        public VehicleType Type => VehicleType.Rover;

        public IVehicle Build(Coordinate coordinate, ISurface surface)
        {
            return new Rover(coordinate, surface);
        }
    }
}