using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;
using MarsExploration.Library.Vehicles;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsExploration.UnitTests
{
    public class VehicleBuilderFactoryTests
    {
        private readonly List<IVehicleBuilder> _buildersMock;

        public VehicleBuilderFactoryTests()
        {
            _buildersMock = new List<IVehicleBuilder>();
        }

        [Fact]
        public void Generate_should_return_builder()
        {
            //Arrange
            var fakeRover = new Mock<IVehicle>();
            var fakeBuilder = new Mock<IVehicleBuilder>();
            fakeBuilder.Setup(x => x.Type).Returns(VehicleType.Rover);
            fakeBuilder.Setup(x => x.Build(It.IsAny<Coordinate>(), It.IsAny<ISurface>())).Returns(fakeRover.Object);
            _buildersMock.Add(fakeBuilder.Object);
            var factory = new VehicleBuilderFactory(_buildersMock);

            //Act
            var builder = factory.Generate(VehicleType.Rover);
            var rover = builder.Build(null, null);

            //Assert
            Assert.NotNull(rover);
        }

        [Fact]
        public void Generate_throws_exception_when_no_builder()
        {
            //Arrange
            var factory = new VehicleBuilderFactory(_buildersMock);

            //Assert
            Assert.Throws<ArgumentException>(() => factory.Generate(VehicleType.Rover));
        }

        [Fact]
        public void Generate_throws_exception_when_builders_null()
        {
            //Arrange
            List<IVehicleBuilder> nullBuilders = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => new VehicleBuilderFactory(nullBuilders));
        }
    }
}