using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;

namespace robot_controller_api.Controllers
{
    [ApiController]
    [Route("api/robot-commands")]
    public class RobotCommandsController : ControllerBase
    {
        private readonly IRobotCommandDataAccess _robotCommandsRepo;
        public RobotCommandsController(IRobotCommandDataAccess robotCommandsRepo)
        {
            _robotCommandsRepo = robotCommandsRepo;
        }
        // Endpoint to fetch all robot commands
        [HttpGet()]
        public IEnumerable<RobotCommand> GetAllRobotCommands()
        {
            // Return all robot commands
            return _robotCommandsRepo.GetRobotCommands();
        }

        // Endpoint to fetch move commands
        [HttpGet("/move")]
        public IEnumerable<RobotCommand> GetMoveCommands()
        {
            return _robotCommandsRepo.GetMoveCommands();
        }
        
        // Endpoint to fetch a command by its ID
        [HttpGet("{id}", Name = "GetRobotCommands")]
        public IActionResult GetCommandsByid(int id)
        {
            var command = _robotCommandsRepo.GetRobotCommandById(id);
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
                _robotCommandsRepo.InsertRobotCommand(newCommand);
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
                _robotCommandsRepo.UpdateRobotCommand(id, newCommand);
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
                _robotCommandsRepo.DeleteRobotCommand(id);
                return Ok("Command deleted successfully");
            }
        }
    }
}
