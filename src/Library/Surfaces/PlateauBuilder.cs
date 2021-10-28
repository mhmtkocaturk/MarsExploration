using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Model.Surfaces;

namespace MarsExploration.Library.Surfaces
{
    public class PlateauBuilder : ISurfaceBuilder
    {
        public SurfaceType Type => SurfaceType.Plateau;

        public ISurface Build(int xLength, int yLength)
        {
            return new Plateau(xLength, yLength);
        }
    }
}