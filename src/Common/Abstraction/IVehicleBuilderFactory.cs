using MarsExploration.Common.Enums;

namespace MarsExploration.Common.Abstraction
{
    public interface IVehicleBuilderFactory
    {
        IVehicleBuilder Generate(VehicleType type);
    }
}