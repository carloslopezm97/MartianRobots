using MartianRobots.Entities;
using MartianRobots.Entities.SQL;
using MartianRobots.Models;
using MartianRobots.Models.Response;
using MartianRobots.Repositories.Interfaces;
using MartianRobots.Services;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Repositories
{
    public class ExecutionRepository : GenericRepository<Execution>, IExecutionRepository
    {
        public ExecutionRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        { }

        /// <summary>
        /// Executes the instruction given to each robot inside a grid.
        /// </summary>
        /// <param name="model">execution model.</param>
        /// <returns></returns>
        public async Task<ExecutionResponseModel> Execute(ExecutionModel model)
        {
            var forbiddenPositions = new List<Position>();
            var executionId = new Guid();

            var grid = new Grid
            {
                Id = new Guid(),
                XSize = model.GridModel.XSize,
                YSize = model.GridModel.YSize,
                ExecutionId = executionId,
            };

            var execution = new Execution
            {
                Id = executionId,
                Grid = grid,
                Robots = new List<Robot>(),
            };

            foreach (var robot in model.RobotModels)
            {
                execution.Robots.Add(robot.ExecuteInstructions(grid, forbiddenPositions));
            }

            var result = await Add(execution);

            var executionModel = new ExecutionResponseModel
            {
                ExecutionId = result.Id,
                Grid = new GridResponseModel
                {
                    Id = result.Grid.Id,
                    XSize = model.GridModel.XSize,
                    YSize = model.GridModel.YSize,
                },
                Robots = new List<RobotResponseModel>(),
            };

            foreach (var robot in result.Robots)
            {
                var maxOrder = robot.PositionRecord.Max(x => x.Order);
                var finalPosition = robot.PositionRecord.Where(x => x.Order == maxOrder).FirstOrDefault();

                executionModel.Robots.Add(new RobotResponseModel
                {
                    Id = robot.Id,
                    Lost = robot.Lost,
                    FinalPosition = new PositionModel
                    {
                        Orientation = finalPosition.Orientation,
                        XCoordinate = finalPosition.XCoordinate,
                        YCoordinate = finalPosition.YCoordinate,
                    }
                });
            }

            return executionModel;
        }

        /// <summary>
        /// Method to get the area explored by the robots given an executionId.
        /// </summary>
        /// <param name="executionId">Id of the execution.</param>
        /// <returns></returns>
        public async Task<List<GridPositionModel>> GetAreaExplored(Guid executionId)
        {
            var positionsList = new List<Position>();
            var execution = await dbSet.Where(x => x.Id == executionId).Include(x => x.Robots).ThenInclude(x => x.PositionRecord).FirstOrDefaultAsync();

            foreach (var robot in execution.Robots)
            {
                positionsList.AddRange(robot.PositionRecord);
            }

            return positionsList.GroupBy(x => new {x.XCoordinate, x.YCoordinate})
                .Select(x => new GridPositionModel
                {
                    XCoordinate = x.Key.XCoordinate,
                    YCoordinate = x.Key.YCoordinate,
                }).OrderBy(x => x.XCoordinate).ThenBy(x => x.YCoordinate).ToList();
        }

        /// <summary>
        /// Method to get a list of the executions done.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExecutionResponseModel>> GetExecutionsPerformed()
        {
            this._logger.LogInformation("Get a list of executions");

            return await dbSet.Include(x => x.Grid).Include(x => x.Robots).ThenInclude(x => x.PositionRecord)
                .Select(x => new ExecutionResponseModel
                {
                    ExecutionId = x.Id,
                    Grid = new GridResponseModel
                    {
                        Id = x.GridId,
                        XSize = x.Grid.XSize,
                        YSize = x.Grid.YSize,
                    },
                    Robots = x.Robots.Select(y => new RobotResponseModel
                        {
                            Id = y.Id,
                            FinalPosition = new PositionModel
                            {
                                Orientation = y.PositionRecord.OrderBy(x => x.Order).Last().Orientation,
                                XCoordinate = y.PositionRecord.OrderBy(x => x.Order).Last().XCoordinate,
                                YCoordinate = y.PositionRecord.OrderBy(x => x.Order).Last().YCoordinate,
                            },
                            Lost = y.Lost
                        }).ToList()
                }).ToListAsync();
        }

        /// <summary>
        /// Method to get the lost robots given an executionId.
        /// </summary>
        /// <param name="executionId">Id of the execution.</param>
        /// <returns></returns>
        public async Task<List<RobotLostResponseModel>> GetLostRobotsByExecutionId(Guid executionId)
        {
            this._logger.LogInformation("Get Lost Robots given an executionId");

            var result = await dbSet.Where(x => x.Id == executionId).Include(x => x.Robots).ThenInclude(x => x.PositionRecord)
                .Select(x => x.Robots.Where(x => x.Lost).Select(y => new RobotLostResponseModel
                {
                    Id = y.Id,
                    FinalPosition = new PositionModel
                    {
                        Orientation = y.PositionRecord.OrderBy(x => x.Order).Last().Orientation,
                        XCoordinate = y.PositionRecord.OrderBy(x => x.Order).Last().XCoordinate,
                        YCoordinate = y.PositionRecord.OrderBy(x => x.Order).Last().YCoordinate,
                    },
                    Lost = y.Lost,
                    InitialPosition = new PositionModel
                    {
                        Orientation = y.PositionRecord.OrderBy(x => x.Order).First().Orientation,
                        XCoordinate = y.PositionRecord.OrderBy(x => x.Order).First().XCoordinate,
                        YCoordinate = y.PositionRecord.OrderBy(x => x.Order).First().YCoordinate,
                    },
                    ListOfInstructions = y.ListOfInstructions
                }).ToList()).FirstOrDefaultAsync();

            return result;
        }
    }
}
