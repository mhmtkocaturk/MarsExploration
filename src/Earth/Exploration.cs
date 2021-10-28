using Grpc.Core;
using Mars;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsExploration.Earth
{
    public class Exploration
    {
        public static async Task StartSample(Rovers.RoversClient client, Metadata? headers, ILogger logger)
        {
            try
            {
                var plateauSizeRequest = new SetPlateauSizeRequest { UpperRightCoordinates = "5 5" };
                await client.SetPlateauSizeAsync(plateauSizeRequest, headers);
                logger.LogInformation(plateauSizeRequest.UpperRightCoordinates);

                var firstRoverInstructions = new SetRoverInstructionsRequest { Coordiate = "1 2 N", Instructions = "LMLMLMLMM" };
                await client.SetRoverInstructionsAsync(firstRoverInstructions, headers);
                var instructions = new StringBuilder();
                instructions.AppendLine(firstRoverInstructions.Coordiate);
                instructions.AppendLine(firstRoverInstructions.Instructions);

                var secondRoverInstructions = new SetRoverInstructionsRequest { Coordiate = "3 3 E", Instructions = "MMRMMRMRRM" };
                await client.SetRoverInstructionsAsync(secondRoverInstructions, headers);
                instructions.AppendLine(secondRoverInstructions.Coordiate);
                instructions.AppendLine(secondRoverInstructions.Instructions);

                logger.LogInformation(instructions.ToString());

                var response = await client.MoveRoversAsync(new MoveRoversRequest(), headers);

                var output = new StringBuilder();
                foreach (var coordinate in response.Coordiates)
                {
                    output.AppendLine(coordinate);
                }

                logger.LogInformation(output.ToString());
            }
            catch (RpcException ex)
            {
                logger.LogError(ex.Status.Detail);
            }
        }

        public static async Task Start(Rovers.RoversClient client, Metadata? headers, ILogger logger,string coordiate, string instructions)
        {
            try
            {                
                var firstRoverInstructions = new SetRoverInstructionsRequest { Coordiate = coordiate, Instructions = instructions };
                await client.SetRoverInstructionsAsync(firstRoverInstructions, headers); 
                
                var response = await client.MoveRoversAsync(new MoveRoversRequest(), headers);

                var output = new StringBuilder();
                foreach (var coordinate in response.Coordiates)
                {
                    output.AppendLine(coordinate);
                }

                logger.LogInformation(output.ToString());
            }
            catch (RpcException ex)
            {
                logger.LogError(ex.Status.Detail);
            }
        }

    }
}
