using Microsoft.AspNetCore.Mvc;
namespace robot_controller_api.Controllers;

// Controller for managing robot commands in the robot controller API
[ApiController]
[Route("api/robot-commands")]
//[Route("api/move")]

public class RobotCommandsController : ControllerBase
{
    // List to store the available robot commands
    private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    {
        // Initializing robot commands
        new RobotCommand(1, "LEFT", false,DateTime.Now ,DateTime.Now, "This command moves the robot to the left"),
        new RobotCommand(2, "RIGHT", false,DateTime.Now,DateTime.Now,"This command moves the robot to the right"),
        new RobotCommand(3, "MOVE" ,  true,DateTime.Now,DateTime.Now,"This command moves the robot"),
        new RobotCommand(4, "PLACE", false,DateTime.Now,DateTime.Now,"This command places the robot"),
        new RobotCommand(5, "REPORT",false,DateTime.Now,DateTime.Now,"This command reports the robot's status")
    };

    // Endpoint to fetch all robot commands
    [HttpGet("/robot-commands")]
    public IEnumerable<RobotCommand> GetAllRobotCommands()
    {
        // Return all available commands
        return _commands;
    }

    // Endpoint to fetch only move commands
    [HttpGet("/move")]
    public IEnumerable<RobotCommand> GetMoveCommandsOnly()
    {
        return _commands.Where(command => command.IsMoveCommand == true);
    }

    // Endpoint to fetch a command by its ID
    [HttpGet("{id}", Name = "GetRobotCommands")]
    public IActionResult GetCommandsByid(int id)
    {
        var command = _commands.FirstOrDefault(cmd => cmd.ID == id);
        if (command != null)
        { 
            return Ok(command);
        }
        else
        {
            return NotFound("Command ID does not exist");
        }
    }

    // Endpoint to add a new command
    [HttpPost]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {
        try
        {
            // Check if the request body contains valid data for a new command
            if (newCommand == null)
            {
                return BadRequest("Please provide valid command details.");
            }

            // Check if a command with the same name already exists
            if (_commands.Any(x => x.Name == newCommand.Name))
            {
                return Conflict($"A command with the name '{newCommand.Name}' already exists.");
            }

            // Generate a new ID for the command
            int newId = _commands.Count+1;

            // Set the ID property of the new command
            newCommand.ID = newId;
            // Set the CreateDate and ModifiedDate properties
            newCommand.CreateDate = DateTime.Now;
            newCommand.ModifiedDate = DateTime.Now;

            // Add the new command to the list
            _commands.Add(newCommand);

            // Return a response indicating successful creation of the new command
            // and provide the URL to retrieve the newly created resource
            return CreatedAtRoute("GetRobotCommands", new { id = newCommand.ID }, newCommand);
        }
        catch (Exception ex)
        {
            // If any exception occurs during the process, return a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }

    // Endpoint to update a command
    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand newCommand)
    {
        if (id == null)
        {
            return BadRequest("ID cannot be null");
        }
        // Check if the provided ID is within the valid range
        if (id >= 1 && id <= _commands.Count)
        {
            var update = _commands.FirstOrDefault(x => x.ID == id);
            newCommand.ID = id;
            _commands[id-1].ID = newCommand.ID;
            _commands[id-1].Name = newCommand.Name;
            _commands[id-1].ModifiedDate = DateTime.Now;
            _commands[id-1].Description = newCommand.Description;
            _commands[id-1].Description = newCommand.Description;
            return NoContent();
        }
        else
        {
            return BadRequest("Command does not exist. Please provide a valid ID.");
        }
    }

    // Endpoint to delete a command
    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {
        if (id == null)
        {
            return BadRequest("ID cannot be empty");
        }
        // Check if the provided ID is within the valid range
        if (id >= 1 && id <= _commands.Count)
        {
            var deleteCommand = _commands.FirstOrDefault(cmd => cmd.ID == id);
            _commands.Remove(deleteCommand);
            return Ok("Command deleted successfully");
        }
        else
        {
            return BadRequest("Command does not exist. Please provide a valid ID.");
        }
    }
}
