using MarsExploration.Common.Location;

namespace MarsExploration.Common.Abstraction
{
    public interface IVehicle
    {
        Coordinate Coordinate { get; }

        void MoveForward();

        void TurnRight();

        void TurnLeft();
    }
}