using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;

namespace robot_controller_api.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        private readonly IMapDataAccess _mapRepo;
        public MapsController(IMapDataAccess mapRepo)
        {
            _mapRepo = mapRepo;
        }
        // Endpoint to fetch all maps
        [HttpGet()]
        public IEnumerable<Map> GetMapsArray()
        {
            return _mapRepo.GetRobotMap();
        }

        // Endpoint to fetch square maps
        [HttpGet("/square")]
        public IEnumerable<Map> GetMapSquare()
        {
            return _mapRepo.GetRobotMapSquare();
        }

        // Endpoint to fetch a map by its ID
        [HttpGet("{id}", Name = "MapsRoute")]
        public IActionResult GetMapByID(int id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null");
            }
            else
            {
                return Ok(_mapRepo.GetMapById(id));
            }
        }

        // Endpoint to add a new map
        [HttpPost("")]
        public IActionResult PostMap(Map map)
        {
            if (map == null)
            {
                return BadRequest("Map cannot be null");
            }
            else
            {
                _mapRepo.PostMap(map);
                return Ok("Map created successfully");
            }
        }

        // Endpoint to update a map
        [HttpPut("{id}")]
        public IActionResult PutMap(int id, Map map)
        {
            if (id == null)
            {
                return BadRequest("Map ID cannot be null");
            }
            else
            {
                _mapRepo.UpdateMap(id, map);
                return Ok("Map updated successfully");
            }
        }

        // Endpoint to delete a map
        [HttpDelete("{id}")]
        public IActionResult DeleteMap(int id)
        {
            if (id == null)
            {
                return NotFound("ID cannot be null for deletion");
            }
            else
            {
                _mapRepo.DeleteMap(id);
                return Ok("Map deleted successfully");
            }
        }

        // Endpoint to check if map index is valid
        [HttpGet("{id}/{x}-{y}")]
        public IActionResult GetMap(int id, int x, int y)
        {
            if (id == null || x == null || y == null)
            {
                return BadRequest("ID, X, and Y cannot be empty");
            }
            else
            {
                return Ok(_mapRepo.IsMapIndexValid(id, x, y));
            }
        }
    }
}
