# MarsExploration

Mars Rover problem solution in C# .NET 5, using gRPC and clean architecture.

In gRPC, a client application can directly call a method on a server application on a different machine as if it were a local object, making it easier for you to create distributed applications and services. As in many RPC systems, gRPC is based around the idea of defining a service, specifying the methods that can be called remotely with their parameters and return types. On the server side, the server implements this interface and runs a gRPC server to handle client calls.
[Introduction to gRPC on .NET](https://docs.microsoft.com/en-us/aspnet/core/grpc/?view=aspnetcore-5.0)


I have followed Command Design Pattern for solving this problem.

The Command Pattern encapsulates a request as an object and gives it a known public interface. A request or action is mapped and stored as an object. The Invoker will be ultimately responsible for processing the request. This clearly decouples the request from the invoker.

# Problem

A squad of robotic rovers are to be landed by NASA on a plateau on Mars.

This plateau, which is curiously rectangular, must be navigated by the rovers so that their on board cameras can get a complete view of the surrounding terrain to send back to Earth.

A rover's position is represented by a combination of an x and y co-ordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot.

'M' means move forward one grid point, and maintain the same heading.

Assume that the square directly North from (x, y) is (x, y+1).

## Input:

The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have been deployed. Each rover has two lines of input. The first line gives the rover's position, and the second line is a series of instructions telling the rover how to explore the plateau.

The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.

Each rover will be finished sequentially, which means that the second rover won't start to move until the first one has finished moving.

## Output:

The output for each rover should be its final co-ordinates and heading.

Test Input:

```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
````

Expected Output:

```
1 3 N
5 1 E
```

# Usage

Application has 2 Console App. 
```
Earth
Rovers
```

Users can start with Earth App with sample data. 
Earth App connects to Rovers and gives sample instructions for Mars Exploration. Users can also add new rovers.
