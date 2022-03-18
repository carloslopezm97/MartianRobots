using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Entities.SQL
{
    /// <summary>
    /// Robot class.
    /// </summary>
    public class Robot
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
        /// The list of positions the robot has been inside the grid.
        /// </summary>
        public List<Position> PositionRecord { get; set; } = new List<Position>();

        /// <summary>
        /// Instructions given to the Robot.
        /// </summary>
        public string ListOfInstructions { get; set; }

        /// <summary>
        /// Id of the execution.
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// Entity of the execution.
        /// </summary>
        public Execution Execution { get; set; }

        /// <summary>
        /// Id of the grid.
        /// </summary>
        public Guid GridId { get; set; }

        /// <summary>
        /// Entity of the grid.
        /// </summary>
        public Grid Grid { get; set; }
    }
}
