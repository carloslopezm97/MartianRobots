using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Entities.SQL
{
    public class Grid
    {
        /// <summary>
        /// Grid identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// X size of the grid.
        /// </summary>
        [Range(0, 50)]
        public int XSize { get; set; }

        /// <summary>
        /// Y size of the grid.
        /// </summary>
        [Range(0, 50)]
        public int YSize { get; set; }

        /// <summary>
        /// List of robots assigned to the grid.
        /// </summary>
        public List<Robot> Robots { get; set; }

        /// <summary>
        /// Id of the execution.
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// Entity of the execution.
        /// </summary>
        public Execution Execution { get; set; }
    }
}
