using MartianRobots.Controllers;
using MartianRobots.Models;
using MartianRobots.Models.Response;
using MartianRobots.Repositories.UnitOfWork;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MartianRobotsTests.IntegrationTests
{
    public class ExecutionControllerTests
    {
        public Mock<IUnitOfWork> unitOfWork;
        public Mock<ILogger<ExecutionController>> logger;
        public ExecutionController executionController;

        public ExecutionControllerTests()
        {
            this.unitOfWork = new Mock<IUnitOfWork>();
            this.logger = new Mock<ILogger<ExecutionController>>();
            this.executionController = new ExecutionController(this.unitOfWork.Object, this.logger.Object);
        }

        [Theory]
        [MemberData(nameof(ExecutionModels))]
        public async Task ExecuteInstructionTest(ExecutionModel model)
        {
            this.unitOfWork.Setup(x => x.ExecutionRepository.Execute(It.IsAny<ExecutionModel>())).ReturnsAsync(new ExecutionResponseModel());

            var result = await this.executionController.Post(model);

            Assert.NotNull(result);
            Assert.IsType<ExecutionResponseModel>(result);
        }

        [Theory]
        [MemberData(nameof(ExecutionWrongModels))]
        public async Task ExecuteInstructionWrongTest(ExecutionModel model)
        {
            this.unitOfWork.Setup(x => x.ExecutionRepository.Execute(It.IsAny<ExecutionModel>())).ReturnsAsync(new ExecutionResponseModel());

            var result = await this.executionController.Post(model);

            Assert.ThrowsAsync<Exception>(async () => await this.executionController.Post(model));
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
                        }
                    }
                }
            }
        };

        public static List<object[]> ExecutionWrongModels => new List<object[]> {
            // Width size of the grid exceeds 50
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = 52,
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
                        }
                    }
                }
            },
            // Heigth size of the grid exceeds 50
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = 5,
                        YSize = 73,
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
                        }
                    }
                }
            },
            // Width size of the grid is lower than 0
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = -5,
                        YSize = 7,
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
                        }
                    }
                }
            },
            // Heigth size is lower than 0
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = 5,
                        YSize = -7,
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
                        }
                    }
                }
            },
            // The instructions list length is over 100
            new object[]
            {
                new ExecutionModel
                {
                    GridModel = new GridModel
                    {
                        XSize = 52,
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
                            ListOfInstructions = "RFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRFRF"
                        }
                    }
                }
            }
        };
    }
}
