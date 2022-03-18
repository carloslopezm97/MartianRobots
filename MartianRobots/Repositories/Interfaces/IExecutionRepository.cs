using MartianRobots.Entities.SQL;
using MartianRobots.Models;
using MartianRobots.Models.Response;

namespace MartianRobots.Repositories.Interfaces
{
    public interface IExecutionRepository : IGenericRepository<Execution>
    {
        Task<ExecutionResponseModel> Execute(ExecutionModel model);

        Task<List<GridPositionModel>> GetAreaExplored(Guid executionId);

        Task<List<ExecutionResponseModel>> GetExecutionsPerformed();

        Task<List<RobotLostResponseModel>> GetLostRobotsByExecutionId(Guid executionId);
    }
}
