using MarsExploration.Common.Abstraction;
using System;

namespace MarsExploration.Library.Commands
{
    public class TurnRight : IVehicleCommand
    {
        private readonly IVehicle _vehicle;

        public TurnRight(IVehicle vehicle)
        {
            _vehicle = vehicle ?? throw new ArgumentNullException($"{nameof(vehicle)} should not be null"); ;
        }

        public void Execute()
        {
            _vehicle.TurnRight();
        }
    }
}