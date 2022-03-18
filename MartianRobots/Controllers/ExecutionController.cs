using MartianRobots.Models;
using MartianRobots.Models.Response;
using MartianRobots.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutionController : ControllerBase
    {
        private readonly ILogger<ExecutionController> logger;
        private readonly IUnitOfWork unitOfWork;

        public ExecutionController(IUnitOfWork unitOfWork, ILogger<ExecutionController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpPost("ExecuteInstruction")]
        public async Task<ExecutionResponseModel> Post( [FromBody] ExecutionModel model)
        {
            this.logger.LogInformation("Execute the robot instructions inside the grid");
            var result = await this.unitOfWork.ExecutionRepository.Execute(model);
            await this.unitOfWork.CompleteAsync();

            return result;
        }

        [HttpGet("AreaExplored/executionId")]
        public async Task<List<GridPositionModel>> Get(Guid executionId)
        {
            this.logger.LogInformation("Get the area explored by the robots given an executionId");
            return await this.unitOfWork.ExecutionRepository.GetAreaExplored(executionId);
        }

        [HttpGet("LostRobots/executionId")]
        public async Task<List<RobotLostResponseModel>> GetLostRobots(Guid executionId)
        {
            this.logger.LogInformation("Get the lost robots given an executionId");
            return await this.unitOfWork.ExecutionRepository.GetLostRobotsByExecutionId(executionId);
        }

        [HttpGet("Executions")]
        public async Task<List<ExecutionResponseModel>> GetExecutionsPerformed()
        {
            this.logger.LogInformation("Get the executions done");
            return await this.unitOfWork.ExecutionRepository.GetExecutionsPerformed();
        }
    }
}
