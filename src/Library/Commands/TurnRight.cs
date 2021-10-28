using MarsExploration.Common.Abstraction;

namespace MarsExploration.Library.Commands
{
    public class TurnRight : IVehicleCommand
    {
        private readonly IVehicle _vehicle;

        public TurnRight(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Execute()
        {
            _vehicle.TurnRight();
        }
    }
}