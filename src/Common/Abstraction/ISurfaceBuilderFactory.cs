using MarsExploration.Common.Enums;

namespace MarsExploration.Common.Abstraction
{
    public interface ISurfaceBuilderFactory
    {
        ISurfaceBuilder Generate(SurfaceType type);
    }
}