namespace MartianRobots.Models.Response
{
    public class ExecutionResponseModel
    {
        public Guid ExecutionId { get; set; }

        public GridResponseModel Grid { get; set; }

        public List<RobotResponseModel> Robots { get; set; }
    }
}
