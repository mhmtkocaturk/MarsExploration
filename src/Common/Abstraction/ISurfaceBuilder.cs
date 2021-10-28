using MarsExploration.Common.Enums;

namespace MarsExploration.Common.Abstraction
{
    public interface ISurfaceBuilder
    {
        SurfaceType Type { get; }

        ISurface Build(int xLength, int yLength);
    }
}