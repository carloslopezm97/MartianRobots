using MartianRobots.Entities;
using MartianRobots.Models;
using MartianRobots.Models.Response;
using MartianRobots.Repositories;
using MartianRobots.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MartianRobotsTests.UnitTests
{
    public  class ExecutionUnitTests : BaseTest
    {
        public IExecutionRepository executionRepository;
        public Mock<ILogger> logger;

        public ExecutionUnitTests() : base()
        {
            this.logger = new Mock<ILogger>();
            this.executionRepository = new ExecutionRepository(applicationDbContext, this.logger.Object);
        }

        [Theory]
        [MemberData(nameof(ExecutionModels))]
        public async Task ExecuteTest(ExecutionModel executionModel, List<RobotResponseModel> robotModels)
        {
            var result = await this.executionRepository.Execute(executionModel);

            for(int i = 0; i < executionModel.RobotModels.Count; i++)
            {
                var robotResponse = result.Robots[i];
                var robotModel = robotModels[i];

                Assert.NotNull(result);
                Assert.NotNull(robotResponse);
                Assert.Equal(robotResponse.FinalPosition.XCoordinate, robotModel.FinalPosition.XCoordinate);
                Assert.Equal(robotResponse.FinalPosition.YCoordinate, robotModel.FinalPosition.YCoordinate);
                Assert.Equal(robotResponse.FinalPosition.Orientation, robotModel.FinalPosition.Orientation);
                Assert.Equal(robotResponse.Lost, robotModel.Lost);
            }
        }

        public static List<object[]> ExecutionModels => new List<object[]> {
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = 5,
                        YSize = 3,
                    },
                    RobotModels = new List<RobotModel>
                    {
                        new RobotModel
                        {
                            InitialPosition = new PositionModel
                            {
                                Orientation = 'E',
                                XCoordinate = 1,
                                YCoordinate = 1
                            },
                            ListOfInstructions = "RFRFRFRF"
                        },
                        new RobotModel
                        {
                            InitialPosition = new PositionModel
                            {
                                Orientation = 'N',
                                XCoordinate = 3,
                                YCoordinate = 2
                            },
                            ListOfInstructions = "FRRFLLFFRRFLL"
                        },
                        new RobotModel
                        {
                            InitialPosition = new PositionModel
                            {
                                Orientation = 'W',
                                XCoordinate = 0,
                                YCoordinate = 3
                            },
                            ListOfInstructions = "LLFFFRFLFL"
                        },
                    }
                },
                new List<RobotResponseModel> {
                    new RobotResponseModel
                    {
                        FinalPosition = new PositionModel
                        {
                            Orientation = 'E',
                            XCoordinate = 1,
                            YCoordinate = 1,
                        },
                        Lost = false,
                    },
                    new RobotResponseModel
                    {
                        FinalPosition = new PositionModel
                        {
                            Orientation = 'N',
                            XCoordinate = 3,
                            YCoordinate = 3,
                        },
                        Lost = true,
                    },
                    new RobotResponseModel
                    {
                        FinalPosition = new PositionModel
                        {
                            Orientation = 'N',
                            XCoordinate = 4,
                            YCoordinate = 2,
                        },
                        Lost = false,
                    }
                }
            }
        };
    }
}
