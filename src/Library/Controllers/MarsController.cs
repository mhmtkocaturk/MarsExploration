using MarsExploration.Common.Abstraction;
using MarsExploration.Common.Contstants;
using MarsExploration.Common.Enums;
using MarsExploration.Common.Location;
using MarsExploration.Library.Commands;
using MarsExploration.Model.Surfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsExploration.Library.Controllers
{
    public class MarsController : IMarsController
    {
        private ISurface _plateau;
        private readonly ISurfaceBuilder _surfaceBuilder;
        private readonly IVehicleBuilder _vehicleBuilder;
        private readonly List<IVehicle> _vehicles = new();
        private readonly List<IVehicleCommand> _commands = new();

        public IEnumerable<IVehicle> Vehicles => _vehicles;

        public MarsController(ISurfaceBuilderFactory surfaceBuilderFactory, IVehicleBuilderFactory vehicleBuilderFactory)
        {
            _surfaceBuilder = surfaceBuilderFactory.Generate(SurfaceType.Plateau);
            _vehicleBuilder = vehicleBuilderFactory.Generate(VehicleType.Rover);
        }

        public async Task SetPlateauSize(string upperRightCoordinates)
        {
            var sizeInfo = await ParseSizeInfo(upperRightCoordinates);
            _plateau = _surfaceBuilder.Build(sizeInfo.XLength, sizeInfo.YLength);
        }

        public async Task SetRoverInstructions(string coordiate, string instructions)
        {
            var coordinate = await ParseCoordinate(coordiate);
            var vehicle = _vehicleBuilder.Build(coordinate, _plateau);
            var commands = GenerateCommands(instructions, vehicle);

            _commands.AddRange(commands);
            _vehicles.Add(vehicle);
        }

        public Task MoveRovers()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }

            _commands.Clear();

            return Task.FromResult(0);
        }

        public Task<List<Coordinate>> GetRoverCoordinates()
        {
            var coordinates = new List<Coordinate>();
            foreach (var vehicle in _vehicles)
            {
                coordinates.Add(vehicle.Coordinate);
            }

            _vehicles.Clear();
            return Task.FromResult(coordinates);
        }

        private Task<Coordinate> ParseCoordinate(string coordiate)
        {
            var values = coordiate.Split(ParameterConstants.SPLITTER);
            if (values.Length == 3)
            {
                if (int.TryParse(values[0], out int x) && int.TryParse(values[1], out int y) && Enum.TryParse(values[2].ToUpper(), out Direction direction))
                {
                    return Task.FromResult(new Coordinate(x, y, direction));
                }
            }
            throw new ArgumentException($"Invalid Coordinate {coordiate}");
        }

        private Task<SizeInfo> ParseSizeInfo(string upperRightCoordinates)
        {
            var values = upperRightCoordinates.Split(ParameterConstants.SPLITTER);
            if (values.Length == 2)
            {
                if (int.TryParse(values[0], out int x) && int.TryParse(values[1], out int y))
                {
                    return Task.FromResult(new SizeInfo(x, y));
                }
            }
            throw new ArgumentException($"Invalid Upper Right Coordinates {upperRightCoordinates}");
        }

        private List<IVehicleCommand> GenerateCommands(string instructions, IVehicle vehicle)
        {
            List<IVehicleCommand> commands = new();
            foreach (var letter in instructions.ToCharArray())
            {
                switch (char.ToUpper(letter))
                {
                    case 'L':
                        commands.Add(new TurnLeft(vehicle));
                        break;

                    case 'R':
                        commands.Add(new TurnRight(vehicle));
                        break;

                    case 'M':
                        commands.Add(new MoveForward(vehicle));
                        break;

                    default:
                        throw new ArgumentException($"Invalid Instructions {instructions}");
                }
            }
            return commands;
        }
    }
}