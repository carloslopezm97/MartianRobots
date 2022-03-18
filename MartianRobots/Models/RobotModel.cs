namespace MartianRobots.Models
{
    public class RobotModel
    {
        /// <summary>
        /// Instructions given to the Robot.
        /// </summary>
        public string ListOfInstructions { get; set; }

        /// <summary>
        /// Initial Position of the Robot.
        /// </summary>
        public PositionModel InitialPosition { get; set; }
    }
}
