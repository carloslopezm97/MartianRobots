namespace MartianRobots.Entities.SQL
{
    public class Execution
    {
        /// <summary>
        /// Id of the execution.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the robot related to the execution.
        /// </summary>
        public Guid GridId { get; set; }

        /// <summary>
        /// Entity of the robot related to the execution.
        /// </summary>
        public Grid Grid { get; set; }

        /// <summary>
        /// List of instructions related to the execution.
        /// </summary>
        public List<Robot> Robots { get; set; } = new List<Robot>();
    }
}
