using MarsExploration.Common.Abstraction;
using System;

namespace MarsExploration.Library.Commands
{
    public class TurnLeft : IVehicleCommand
    {
        private readonly IVehicle _vehicle;

        public TurnLeft(IVehicle vehicle)
        {
            _vehicle = vehicle ?? throw new ArgumentNullException($"{nameof(vehicle)} should not be null"); ;
        }

        public void Execute()
        {
            _vehicle.TurnLeft();
        }
    }
}