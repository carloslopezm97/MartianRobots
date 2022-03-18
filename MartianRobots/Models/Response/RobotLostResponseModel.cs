namespace MartianRobots.Models.Response
{
    public class RobotLostResponseModel
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

        /// <summary>
        /// Initial Position of the Robot.
        /// </summary>
        public PositionModel InitialPosition { get; set; }

        /// <summary>
        /// Instructions given to the Robot.
        /// </summary>
        public string ListOfInstructions { get; set; }
    }
}
