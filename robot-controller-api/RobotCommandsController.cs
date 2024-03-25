using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;

namespace robot_controller_api.Controllers
{
    [ApiController]
    [Route("api/robot-commands")]
    public class RobotCommandsController : ControllerBase
    {
        // Endpoint to fetch all robot commands
        [HttpGet()]
        public IEnumerable<RobotCommand> GetAllRobotCommands()
        {
            // Return all robot commands
            return RobotCommandDataAccess.GetRobotCommands();
        }

        // Endpoint to fetch move commands
        [HttpGet("/move")]
        public IEnumerable<RobotCommand> GetMoveCommands()
        {
            return RobotCommandDataAccess.GetMoveCommands();
        }
        
        // Endpoint to fetch a command by its ID
        [HttpGet("{id}", Name = "GetRobotCommands")]
        public IActionResult GetCommandsByid(int id)
        {
            var command = RobotCommandDataAccess.GetRobotCommandById(id);
            if (command != null)
            {
                return Ok(command);
            }
            else
            {
                return NotFound("ID does not exist");
            }
        }

        // Endpoint to add a new command
        [HttpPost("")]
        public IActionResult AddRobotCommand(RobotCommand newCommand)
        {
            if (newCommand == null)
            {
                return BadRequest("The command cannot be null");
            }
            else
            {
                RobotCommandDataAccess.InsertRobotCommand(newCommand);
                return Ok("Command created successfully");
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
            else
            {
                RobotCommandDataAccess.UpdateRobotCommand(id, newCommand);
                return Ok("Command updated successfully");
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
            else
            {
                RobotCommandDataAccess.DeleteRobotCommand(id);
                return Ok("Command deleted successfully");
            }
        }
    }
}
