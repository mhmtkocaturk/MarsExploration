using MarsExploration.Common.Enums;

namespace MarsExploration.Common.Location
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public Coordinate(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Direction}";
        }
    }
}