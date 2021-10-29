using MarsExploration.Common.Abstraction;
using MarsExploration.Library.Commands;
using Moq;
using System;
using Xunit;

namespace MarsExploration.UnitTests
{
    public class CommandsTests
    {
        private readonly Mock<IVehicle> fakeRover;

        public CommandsTests()
        {
            fakeRover = new Mock<IVehicle>();
        }

        [Fact]
        public void MoveForward_throws_exception_when_vehicle_null()
        {
            //Arrange
            IVehicle nullVehicle = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() => new MoveForward(nullVehicle));
        }

        [Fact]
        public void TurnLeft_throws_exception_when_vehicle_null()
        {
            //Arrange
            IVehicle nullVehicle = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() => new TurnLeft(nullVehicle));
        }

        [Fact]
        public void TurnRight_throws_exception_when_vehicle_null()
        {
            //Arrange
            IVehicle nullVehicle = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() => new TurnRight(nullVehicle));
        }

        [Fact]
        public void MoveForward_should_success()
        {
            //Arrange
            fakeRover.Setup(x => x.MoveForward()).Verifiable();
            var command = new MoveForward(fakeRover.Object);

            //Act
            command.Execute();

            //Assert
            fakeRover.Verify(x => x.MoveForward(), Times.Once);
        }

        [Fact]
        public void TurnLeft_should_success()
        {
            //Arrange
            fakeRover.Setup(x => x.TurnLeft()).Verifiable();
            var command = new TurnLeft(fakeRover.Object);

            //Act
            command.Execute();

            //Assert
            fakeRover.Verify(x => x.TurnLeft(), Times.Once);
        }

        [Fact]
        public void TurnRight_should_success()
        {
            //Arrange
            fakeRover.Setup(x => x.TurnRight()).Verifiable();
            var command = new TurnRight(fakeRover.Object);

            //Act
            command.Execute();

            //Assert
            fakeRover.Verify(x => x.TurnRight(), Times.Once);
        }
    }
}