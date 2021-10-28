using MarsExploration.Common.Abstraction;

namespace MarsExploration.Library.Commands
{
    public class TurnLeft : IVehicleCommand
    {
        private readonly IVehicle _vehicle;

        public TurnLeft(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Execute()
        {
            _vehicle.TurnLeft();
        }
    }
}