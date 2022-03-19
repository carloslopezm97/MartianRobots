using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Models
{
    public class RobotModel
    {
        /// <summary>
        /// Instructions given to the Robot.
        /// </summary>
        [StringLength(100)]
        public string ListOfInstructions { get; set; }

        /// <summary>
        /// Initial Position of the Robot.
        /// </summary>
        public PositionModel InitialPosition { get; set; }
    }
}
