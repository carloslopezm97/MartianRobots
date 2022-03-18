namespace MartianRobots.Models
{
    public class ExecutionModel
    {
        /// <summary>
        /// Grid model of the execution.
        /// </summary>
        public GridModel GridModel { get; set; }

        /// <summary>
        /// List of Robot Models of the execution.
        /// </summary>
        public List<RobotModel> RobotModels { get; set; }
    }
}
