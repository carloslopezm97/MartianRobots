using MartianRobots.Entities.SQL;
using MartianRobots.Models;

namespace MartianRobots.Services
{
    public static class ExecutionServices
    {
        /// <summary>
        /// Method to execute the list of instructions given to the robot.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="gridModel"></param>
        /// <returns></returns>
        public static Robot ExecuteInstructions(this RobotModel model, Grid grid, List<Position> forbiddenPositions)
        {
            var robot = new Robot
            {
                Id = new Guid(),
                ListOfInstructions = model.ListOfInstructions,
                PositionRecord = new List<Position> {
                    new Position
                    {
                        Id = new Guid(),
                        Order = 0,
                        Orientation = model.InitialPosition.Orientation,
                        XCoordinate = model.InitialPosition.XCoordinate,
                        YCoordinate = model.InitialPosition.YCoordinate,
                    }
                },
                Grid = grid
            };

            var currentPosition = model.InitialPosition;
            var order = 1;

            foreach (var instruction in model.ListOfInstructions)
            {
                currentPosition = currentPosition.ExecuteInstruction(instruction, forbiddenPositions);

                if (currentPosition.IsRobotLost(grid))
                {
                    robot.Lost = true;
                    forbiddenPositions.Add(robot.PositionRecord.Last());
                    break;
                }

                robot.PositionRecord.Add(new Position
                {
                    Id = new Guid(),
                    Order = order++,
                    Orientation = currentPosition.Orientation,
                    XCoordinate = currentPosition.XCoordinate,
                    YCoordinate = currentPosition.YCoordinate,
                });
            }

            return robot;
        }

        /// <summary>
        /// Method to execute the instruction given to the robot.
        /// </summary>
        /// <param name="currentPosition">Current position of the Robot.</param>
        /// <param name="instruction">Instruction given to update the Position.</param>
        /// <returns></returns>
        private static PositionModel ExecuteInstruction(this PositionModel currentPosition, char instruction, List<Position> forbiddenPositions)
        {
            switch (instruction)
            {
                case InstructionConstants.Right:
                case InstructionConstants.Left:
                    currentPosition.Orientation = currentPosition.Orientation.ChangeOrientation(instruction);
                    break;
                case InstructionConstants.Forward:
                    if(!forbiddenPositions.Where(x => x.XCoordinate == currentPosition.XCoordinate 
                        && x.YCoordinate == currentPosition.YCoordinate 
                        && x.Orientation == currentPosition.Orientation).Any())
                    {
                        currentPosition.MoveForward();
                    }
                    break;
            }

            return currentPosition;
        }

        private static char ChangeOrientation(this char currentOrientation, char instruction)
        {
            switch (currentOrientation)
            {
                case OrientationConstants.North:
                    if (instruction == InstructionConstants.Right) return OrientationConstants.East;
                    else if (instruction == InstructionConstants.Left) return OrientationConstants.West;
                    break;
                case OrientationConstants.East:
                    if (instruction == InstructionConstants.Right) return OrientationConstants.South;
                    else if (instruction == InstructionConstants.Left) return OrientationConstants.North;
                    break;
                case OrientationConstants.South:
                    if (instruction == InstructionConstants.Right) return OrientationConstants.West;
                    else if (instruction == InstructionConstants.Left) return OrientationConstants.East;
                    break;
                case OrientationConstants.West:
                    if (instruction == InstructionConstants.Right) return OrientationConstants.North;
                    else if (instruction == InstructionConstants.Left) return OrientationConstants.South;
                    break;
            }

            return currentOrientation;
        }

        /// <summary>
        /// Method to update the current Position of the Robot.
        /// </summary>
        /// <param name="currentPosition">Current Position of the Robot.</param>
        /// <returns></returns>
        private static PositionModel MoveForward(this PositionModel currentPosition)
        {
            switch (currentPosition.Orientation)
            {
                case OrientationConstants.North:
                    currentPosition.YCoordinate++;
                    break;
                case OrientationConstants.East:
                    currentPosition.XCoordinate++;
                    break;
                case OrientationConstants.South:
                    currentPosition.YCoordinate--;
                    break;
                case OrientationConstants.West:
                    currentPosition.XCoordinate--;
                    break;
            }

            return currentPosition;
        }

        /// <summary>
        /// Method to check if the robot is lost.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gridModel"></param>
        /// <returns></returns>
        private static bool IsRobotLost(this PositionModel position, Grid grid)
        {
            switch (position.Orientation)
            {
                case OrientationConstants.North:
                    if (position.YCoordinate > grid.YSize) return true;
                    break;
                case OrientationConstants.South:
                    if (position.YCoordinate < 0) return true;
                    break;
                case OrientationConstants.East:
                    if (position.XCoordinate > grid.XSize) return true;
                    break;
                case OrientationConstants.West:
                    if (position.XCoordinate < 0) return true;
                    break;
            }

            return false;
        }
    }
}
