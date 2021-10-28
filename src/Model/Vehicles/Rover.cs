using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;
using System;

namespace MarsExploration.Model.Vehicles
{
    public class Rover : IVehicle
    {
        public Coordinate Coordinate { get; private set; }
        public ISurface Surface { get; private set; }

        public Rover(Coordinate coordinate, ISurface surface)
        {
            Coordinate = coordinate;
            Surface = surface;
        }

        public void MoveForward()
        {
            switch (Coordinate.Direction)
            {
                case Direction.N:
                    Coordinate.Y += 1;
                    break;

                case Direction.S:
                    Coordinate.Y -= 1;
                    break;

                case Direction.E:
                    Coordinate.X += 1;
                    break;

                case Direction.W:
                    Coordinate.X -= 1;
                    break;
            }

            if (!Surface.Contains(Coordinate))
            {
                throw new Exception($"Rover Connection Lost. Last Coordinate : {Coordinate} ");
            }
        }

        public void TurnLeft()
        {
            switch (Coordinate.Direction)
            {
                case Direction.N:
                    Coordinate.Direction = Direction.W;
                    break;

                case Direction.S:
                    Coordinate.Direction = Direction.E;
                    break;

                case Direction.E:
                    Coordinate.Direction = Direction.N;
                    break;

                case Direction.W:
                    Coordinate.Direction = Direction.S;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Coordinate.Direction)
            {
                case Direction.N:
                    Coordinate.Direction = Direction.E;
                    break;

                case Direction.S:
                    Coordinate.Direction = Direction.W;
                    break;

                case Direction.E:
                    Coordinate.Direction = Direction.S;
                    break;

                case Direction.W:
                    Coordinate.Direction = Direction.N;
                    break;
            }
        }
    }
}