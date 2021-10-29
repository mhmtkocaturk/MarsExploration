using Mars;
using MarsExploration.FuntionalTests.Base;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsExploration.FuntionalTests
{
    [TestFixture]
    public class RoverScenarios : ScenarioBase
    {
        [Test]
        public async Task SetPlateauSize_should_return_success()
        {
            // Arrange
            var client = new Mars.Rovers.RoversClient(Channel);

            // Act
            var plateauSizeRequest = new SetPlateauSizeRequest { UpperRightCoordinates = "5 5" };
            var response = await client.SetPlateauSizeAsync(plateauSizeRequest);

            // Assert
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task SetRoverInstructions_should_return_success()
        {
            // Arrange
            var client = new Mars.Rovers.RoversClient(Channel);

            // Act
            var firstRoverInstructions = new SetRoverInstructionsRequest { Coordiate = "1 2 N", Instructions = "LMLMLMLMM" };
            var response = await client.SetRoverInstructionsAsync(firstRoverInstructions);

            // Assert
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task MoveRovers_should_return_coordinates()
        {
            // Arrange
            var client = new Mars.Rovers.RoversClient(Channel);
            await client.SetPlateauSizeAsync(new SetPlateauSizeRequest { UpperRightCoordinates = "5 5" });
            await client.SetRoverInstructionsAsync(new SetRoverInstructionsRequest { Coordiate = "1 2 N", Instructions = "LMLMLMLMM" });

            // Act
            var response = await client.MoveRoversAsync(new MoveRoversRequest());

            // Assert
            Assert.IsNotNull(response.Coordiates);
            Assert.IsTrue(response.Coordiates.Any());
        }

        [Test, TestCaseSource("RoversTestData")]
        public async Task Rovers_coordinates_should_yield(string coordinate, string instructions, string expectedCoordinate)
        {
            // Arrange
            var client = new Mars.Rovers.RoversClient(Channel);
            await client.SetPlateauSizeAsync(new SetPlateauSizeRequest { UpperRightCoordinates = "5 5" });
            await client.SetRoverInstructionsAsync(new SetRoverInstructionsRequest { Coordiate = coordinate, Instructions = instructions });

            // Act
            var response = await client.MoveRoversAsync(new MoveRoversRequest());

            // Assert
            Assert.IsNotNull(response.Coordiates);
            Assert.IsTrue(response.Coordiates.Any());
            Assert.That(response.Coordiates.First(), Is.EqualTo(expectedCoordinate));
        }

        private static IEnumerable<TestCaseData> RoversTestData()
        {
            yield return new TestCaseData("1 2 N", "LMLMLMLMM", "1 3 N");
            yield return new TestCaseData("3 3 E", "MMRMMRMRRM", "5 1 E");
            yield return new TestCaseData("5 0 W", "MMMMMR", "0 0 N");
            yield return new TestCaseData("5 5 S", "MM", "5 3 S");
            yield return new TestCaseData("1 1 W", "RMRMLM", "2 3 N");
            yield return new TestCaseData("4 2 N", "LMMML", "1 2 S");
            yield return new TestCaseData("3 3 E", "LLMLMMMRMM", "0 0 W");
            yield return new TestCaseData("5 3 E", "LLRMMRRM", "5 4 S");
            yield return new TestCaseData("4 1 W", "MMMMRRRRR", "0 1 N");
            yield return new TestCaseData("0 0 W", "LLMMMMMRRRMMMMMLMMMMMLMMMMM", "0 0 S");
        }
    }
}