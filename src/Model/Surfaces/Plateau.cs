using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Location;

namespace MarsExploration.Model.Surfaces
{
    public class Plateau : ISurface
    {
        public SizeInfo Size { get; }

        public Plateau(int xLength, int yLength) => Size = new SizeInfo(xLength, yLength);

        public bool Contains(Coordinate coordinate) => coordinate.X <= Size.XLength && coordinate.Y <= Size.YLength;
    }
}