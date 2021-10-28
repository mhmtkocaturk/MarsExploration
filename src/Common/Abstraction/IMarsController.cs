using MarsExploration.Common.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsExploration.Common.Abstraction
{
    public interface IMarsController
    {
        Task SetPlateauSize(string upperRightCoordinates);

        Task SetRoverInstructions(string coordiate, string instructions);

        Task MoveRovers();

        Task<List<Coordinate>> GetRoverCoordinates();
    }
}