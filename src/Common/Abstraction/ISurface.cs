using MarsExploration.Common.Location;

namespace MarsExploration.Common.Abstraction
{
    public interface ISurface
    {
        public bool Contains(Coordinate coordinate);
    }
}