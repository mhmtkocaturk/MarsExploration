using MarsExploration.Common.Abstraction;

namespace MarsExploration.Library.Commands
{
    public class MoveForward : IVehicleCommand
    {
        private readonly IVehicle _vehicle;

        public MoveForward(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Execute()
        {
            _vehicle.MoveForward();
        }
    }
}