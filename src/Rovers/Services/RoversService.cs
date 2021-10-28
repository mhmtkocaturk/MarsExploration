using Grpc.Core;
using Mars;
using MarsExploration.Common.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MarsExploration.Rovers.Services
{
    public class RoversService : Mars.Rovers.RoversBase
    {
        private readonly IMarsController _marsController;
        private readonly ILogger<RoversService> _logger;

        public RoversService(IMarsController marsController, ILogger<RoversService> logger)
        {
            _marsController = marsController;
            _logger = logger;
        }

        public override async Task<SetPlateauSizeResponse> SetPlateauSize(SetPlateauSizeRequest request, ServerCallContext context)
        {
            try
            {
                await _marsController.SetPlateauSize(request.UpperRightCoordinates);
                _logger.LogInformation(request.UpperRightCoordinates);

                return new SetPlateauSizeResponse { Success = true };
            }
            catch (Exception ex)
            {
                context.Status = new Status(StatusCode.InvalidArgument, ex.Message);
                return new SetPlateauSizeResponse { Success = false };
            }
         
        }

        public override async Task<SetRoverInstructionsResponse> SetRoverInstructions(SetRoverInstructionsRequest request, ServerCallContext context)
        {            
            try
            {
                await _marsController.SetRoverInstructions(request.Coordiate, request.Instructions);
                var instructions = new StringBuilder();
                instructions.AppendLine(request.Coordiate);
                instructions.AppendLine(request.Instructions);
                _logger.LogInformation(instructions.ToString());
                return new SetRoverInstructionsResponse { Success = true };
            }
            catch (Exception  ex)
            {
                context.Status = new Status(StatusCode.InvalidArgument, ex.Message);
                return new SetRoverInstructionsResponse { Success = false };
            }  
        }

        public override async Task<MoveRoversResponse> MoveRovers(MoveRoversRequest request, ServerCallContext context)
        {
            var response = new MoveRoversResponse();
            try
            {
                await _marsController.MoveRovers();
                var coordinates = await _marsController.GetRoverCoordinates();

               
                var output = new StringBuilder();
                foreach (var coordinate in coordinates)
                {
                    output.AppendLine(coordinate.ToString());
                    response.Coordiates.Add(coordinate.ToString());
                }

                _logger.LogInformation(output.ToString());                
            }
            catch (Exception ex )
            {
                context.Status = new Status(StatusCode.InvalidArgument, ex.Message); 
            }
            return response; 
        }
    }
}