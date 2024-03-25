using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;

namespace robot_controller_api.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        /// <summary>
        /// Endpoint to fetch all maps.
        /// </summary>
        /// <returns>All maps.</returns>
        [HttpGet]
        public IEnumerable<Map> GetMapsArray()
        {
            return MapDataAccess.GetRobotMap();
        }

        /// <summary>
        /// Endpoint to fetch square maps.
        /// </summary>
        /// <returns>All square maps.</returns>
        [HttpGet("square")]
        public IEnumerable<Map> GetMapSquare()
        {
            return MapDataAccess.GetRobotMapSquare();
        }

        /// <summary>
        /// Endpoint to fetch a map by its ID.
        /// </summary>
        /// <param name="id">The ID of the map.</param>
        /// <returns>The map with the specified ID.</returns>
        [HttpGet("{id}", Name = "MapsRoute")]
        public IActionResult GetMapByID(int id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null");
            }
            else
            {
                return Ok(MapDataAccess.GetMapById(id));
            }
        }

        /// <summary>
        /// Endpoint to add a new map.
        /// </summary>
        /// <param name="map">The new map.</param>
        /// <returns>The newly created map.</returns>
        [HttpPost]
        public IActionResult PostMap(Map map)
        {
            if (map == null)
            {
                return BadRequest("Map cannot be null");
            }
            else
            {
                MapDataAccess.PostMap(map);
                return Ok("Map created successfully");
            }
        }

        /// <summary>
        /// Endpoint to update a map.
        /// </summary>
        /// <param name="id">The ID of the map to update.</param>
        /// <param name="map">The updated map data.</param>
        /// <returns>Status code indicating success or failure.</returns>
        [HttpPut("{id}")]
        public IActionResult PutMap(int id, Map map)
        {
            if (id == null)
            {
                return BadRequest("Map ID cannot be null");
            }
            else
            {
                MapDataAccess.UpdateMap(id, map);
                return Ok("Map updated successfully");
            }
        }

        /// <summary>
        /// Endpoint to delete a map.
        /// </summary>
        /// <param name="id">The ID of the map to delete.</param>
        /// <returns>Status code indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteMap(int id)
        {
            if (id == null)
            {
                return NotFound("ID cannot be null for deletion");
            }
            else
            {
                MapDataAccess.DeleteMap(id);
                return Ok("Map deleted successfully");
            }
        }

        /// <summary>
        /// Endpoint to check if map index is valid.
        /// </summary>
        /// <param name="id">The ID of the map.</param>
        /// <param name="x">The X coordinate of the map index.</param>
        /// <param name="y">The Y coordinate of the map index.</param>
        /// <returns>True if the map index is valid, false otherwise.</returns>
        [HttpGet("{id}/{x}-{y}")]
        public IActionResult GetMap(int id, int x, int y)
        {
            if (id == null || x == null || y == null)
            {
                return BadRequest("ID, X, and Y cannot be empty");
            }
            else
            {
                return Ok(MapDataAccess.IsMapIndexValid(id, x, y));
            }
        }
    }
}
