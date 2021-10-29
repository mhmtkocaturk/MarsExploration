using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;
using MarsExploration.Library.Controllers;
using MarsExploration.Library.Surfaces;
using MarsExploration.Library.Vehicles;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarsExploration.UnitTests
{
    public class MarsControllerTests
    {
        private readonly List<ISurfaceBuilder> _surfaceBuildersMock;
        private readonly List<IVehicleBuilder> _vehicleBuildersMock;

        public MarsControllerTests()
        {
            _surfaceBuildersMock = new List<ISurfaceBuilder>();
            _vehicleBuildersMock = new List<IVehicleBuilder>();
        }

        [Fact]
        public async Task SetPlateauSize_should_success()
        {
            //Arrange
            var vehicleBuilderFactory = GenerateMockVehicleFactory();
            var surfaceBuilderFactory = GenerateMockSurfaceFactory();

            var controller = new MarsController(surfaceBuilderFactory, vehicleBuilderFactory);

            //Act
            await controller.SetPlateauSize("5 5");

            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task SetRoverInstructions_should_success()
        {
            //Arrange
            var vehicleBuilderFactory = GenerateMockVehicleFactory();
            var surfaceBuilderFactory = GenerateMockSurfaceFactory();

            var controller = new MarsController(surfaceBuilderFactory, vehicleBuilderFactory);

            //Act
            await controller.SetRoverInstructions("1 2 N", "LMLMLMLMM");

            //Assert
            Assert.True(controller.Vehicles.Any());
        }

        [Fact]
        public async Task RoversCoordinate_should_be()
        {
            //Arrange
            var vehicleBuilderFactory = GenerateMockVehicleFactory(new Coordinate(1, 3, Direction.N));
            var surfaceBuilderFactory = GenerateMockSurfaceFactory();

            var controller = new MarsController(surfaceBuilderFactory, vehicleBuilderFactory);

            //Act
            await controller.SetRoverInstructions("1 2 N", "LMLMLMLMM");
            await controller.MoveRovers();
            var coordinates = await controller.GetRoverCoordinates();

            //Assert
            Assert.Equal("1 3 N", coordinates.First().ToString());
        }

        private SurfaceBuilderFactory GenerateMockSurfaceFactory()
        {
            var fakePlateau = new Mock<ISurface>();
            var fakeSurfaceBuilder = new Mock<ISurfaceBuilder>();
            fakeSurfaceBuilder.Setup(x => x.Type).Returns(SurfaceType.Plateau);
            fakeSurfaceBuilder.Setup(x => x.Build(It.IsAny<int>(), It.IsAny<int>())).Returns(fakePlateau.Object);
            _surfaceBuildersMock.Add(fakeSurfaceBuilder.Object);
            var surfaceBuilderFactory = new SurfaceBuilderFactory(_surfaceBuildersMock);
            return surfaceBuilderFactory;
        }

        private VehicleBuilderFactory GenerateMockVehicleFactory(Coordinate coordinate = null)
        {
            var fakeRover = new Mock<IVehicle>();
            fakeRover.Setup(x => x.Coordinate).Returns(coordinate);
            var fakeVehicleBuilder = new Mock<IVehicleBuilder>();
            fakeVehicleBuilder.Setup(x => x.Type).Returns(VehicleType.Rover);
            fakeVehicleBuilder.Setup(x => x.Build(It.IsAny<Coordinate>(), It.IsAny<ISurface>())).Returns(fakeRover.Object);
            _vehicleBuildersMock.Add(fakeVehicleBuilder.Object);
            var vehicleBuilderFactory = new VehicleBuilderFactory(_vehicleBuildersMock);
            return vehicleBuilderFactory;
        }
    }
}