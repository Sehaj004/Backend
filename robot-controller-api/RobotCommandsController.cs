using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;

namespace robot_controller_api.Controllers
{
    [ApiController]
    [Route("api/robot-commands")]
    public class RobotCommandsController : ControllerBase
    {
        /// <summary>
        /// Endpoint to fetch all robot commands.
        /// </summary>
        /// <returns>All robot commands.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<RobotCommand> GetAllRobotCommands()
        {
            return RobotCommandDataAccess.GetRobotCommands();
        }

        /// <summary>
        /// Endpoint to fetch move commands.
        /// </summary>
        /// <returns>All move commands.</returns>
        [HttpGet("move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<RobotCommand> GetMoveCommands()
        {
            return RobotCommandDataAccess.GetMoveCommands();
        }
        
        /// <summary>
        /// Endpoint to fetch a command by its ID.
        /// </summary>
        /// <param name="id">The ID of the command.</param>
        /// <returns>The command with the specified ID.</returns>
        [HttpGet("{id}", Name = "GetRobotCommands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Endpoint to add a new command.
        /// </summary>
        /// <param name="newCommand">The new robot command.</param>
        /// <returns>The newly created robot command.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        /// <summary>
        /// Endpoint to update a command.
        /// </summary>
        /// <param name="id">The ID of the command to update.</param>
        /// <param name="newCommand">The updated command data.</param>
        /// <returns>Status code indicating success or failure.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRobotCommand(int id, RobotCommand newCommand)
        {
            if (newCommand == null)
            {
                return BadRequest("Invalid command data");
            }
            else
            {
                RobotCommandDataAccess.UpdateRobotCommand(id, newCommand);
                return Ok("Command updated successfully");
            }
        }

        /// <summary>
        /// Endpoint to delete a command.
        /// </summary>
        /// <param name="id">The ID of the command to delete.</param>
        /// <returns>Status code indicating success or failure.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRobotCommand(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            else
            {
                RobotCommandDataAccess.DeleteRobotCommand(id);
                return Ok("Command deleted successfully");
            }
        }
    }
}
