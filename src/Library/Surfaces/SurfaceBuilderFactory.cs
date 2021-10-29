using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsExploration.Library.Surfaces
{
    public class SurfaceBuilderFactory : ISurfaceBuilderFactory
    {
        private readonly IEnumerable<ISurfaceBuilder> _surfaceBuilders;

        public SurfaceBuilderFactory(IEnumerable<ISurfaceBuilder> surfaceBuilders)
        {
            _surfaceBuilders = surfaceBuilders ?? throw new ArgumentNullException($"{nameof(surfaceBuilders)} should not be null");
        }

        public ISurfaceBuilder Generate(SurfaceType type)
        {
            var surfaceBuilder = _surfaceBuilders.FirstOrDefault(builder => builder.Type == type);

            return surfaceBuilder ?? throw new ArgumentException($"There is not defined surface builder for [{type}]");
        }
    }
}