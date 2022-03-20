# MartianRobots
Martian Robots is a REST API developed in .net 6.
The ORM used in the project is Entity Framework Core and the database creation method was developing first the models and dependencies in the project and then adding the option EnsureCreated to create the database automatically whenever the application is executed.
In order to execute the application, SQL Server must be installed and the database will be automatically created in the local server of your computer with the name MartianRobotsDB.
Swagger has been implemented in the application in order to test the endpoints. However, it can be tested with any other tool such as Postman and aim to the url specified in the file launchSettings.json.
The project has a repository pattern developed that provides an abstraction layer between the database access and business logic.
The project has been developed to be able to reuse a lot of code and make easier the next developments. Moreover, the project classes and methods have been completely commented describing their functionality.
XUnit test project has been added with unit and integration tests.


The main endpoint to execute the challenge described is the endpoint "/ExecuteInstruction" which gets a model with the required information. Instead of the example described in the challange, the endpoint receive a model with the following properties:
{
  "gridModel": {
    "xSize": 50,
    "ySize": 50
  },
  "robotModels": [
    {
      "listOfInstructions": "string",
      "initialPosition": {
        "xCoordinate": 0,
        "yCoordinate": 0,
        "orientation": "string"
      }
    }
  ]
}
The gridModel with the properties to describe the size of the planet and the robotModels which is a list of robots with the listOfInstructions as the string of the movements the robot has to perform and the initial position with the parameters of xCoordinate, yCoordinate to specify the starting point of the robot and Orientation that can be "N", "E", "W" and "S" as the challenge described.

This endpoint will execute the challenge and store in the database everything needed. The endpoint will return another model with the following properties:
{
  "executionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "grid": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "xSize": 0,
    "ySize": 0
  },
  "robots": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "lost": true,
      "finalPosition": {
        "xCoordinate": 0,
        "yCoordinate": 0,
        "orientation": "string"
      }
    }
  ]
}
First, the executionId of the execution which can be used in the other endpoints to retrieve data about this execution, the grid properties and a list of robots which properties are id of the robot, a lost property to specify if the robot has been lost and the final position of the robot at the end of the execution.

There are other endpoints developed to get a list of executions performed, get the area explored given an executionId and get the lost robots of an execution given an executionId.
