using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;

namespace MarsExploration.Common.Abstraction
{
    public interface IVehicleBuilder
    {
        VehicleType Type { get; }

        IVehicle Build(Coordinate coordinate, ISurface surface);
    }
}