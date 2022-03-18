namespace MartianRobots.Models.Response
{
    public class RobotResponseModel
    {
        /// <summary>
        /// Robot identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Determines if the robot is outside the grid.
        /// </summary>
        public bool Lost { get; set; }

        /// <summary>
        /// Final Position of the Robot.
        /// </summary>
        public PositionModel FinalPosition { get; set; }
    }
}
