using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Library.Surfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsExploration.UnitTests
{
    public class SurfaceBuilderFactoryTests
    {
        private readonly List<ISurfaceBuilder> _buildersMock;

        public SurfaceBuilderFactoryTests()
        {
            _buildersMock = new List<ISurfaceBuilder>();
        }

        [Fact]
        public void Generate_should_return_builder()
        {
            //Arrange
            var fakePlateau = new Mock<ISurface>();
            var fakeBuilder = new Mock<ISurfaceBuilder>();
            fakeBuilder.Setup(x => x.Type).Returns(SurfaceType.Plateau);
            fakeBuilder.Setup(x => x.Build(It.IsAny<int>(), It.IsAny<int>())).Returns(fakePlateau.Object);
            _buildersMock.Add(fakeBuilder.Object);
            var factory = new SurfaceBuilderFactory(_buildersMock);

            //Act
            var builder = factory.Generate(SurfaceType.Plateau);
            var plateau = builder.Build(0, 0);

            //Assert
            Assert.NotNull(plateau);
        }

        [Fact]
        public void Generate_throws_exception_when_no_builder()
        {
            //Arrange
            var factory = new SurfaceBuilderFactory(_buildersMock);

            //Assert
            Assert.Throws<ArgumentException>(() => factory.Generate(SurfaceType.Plateau));
        }

        [Fact]
        public void Generate_throws_exception_when_builders_null()
        {
            //Arrange
            List<ISurfaceBuilder> nullBuilders = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => new SurfaceBuilderFactory(nullBuilders));
        }
    }
}