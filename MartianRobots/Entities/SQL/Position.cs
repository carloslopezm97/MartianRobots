using MartianRobots.Models;
using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Entities.SQL
{
    /// <summary>
    /// Position class.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Position identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// X coordinate.
        /// </summary>
        public int XCoordinate { get; set; }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int YCoordinate { get; set; }

        /// <summary>
        /// Orientation of the position.
        /// </summary>
        public char Orientation { get; set; }

        /// <summary>
        /// Id of the Robot the position belongs to.
        /// </summary>
        public Guid RobotId { get; set; }

        /// <summary>
        /// Robot entity.
        /// </summary>
        public Robot Robot { get; set; }

        /// <summary>
        /// Order of the position in an instruction.
        /// </summary>
        public int Order { get; set; }
    }
}
