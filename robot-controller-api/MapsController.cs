using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Controllers
{
    // Controller for managing maps in the robot controller API
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        // List to store multiple maps
        private static readonly List<Map> _maps = new List<Map>
        {
            // Initializing maps with different dimensions
            new Map(1, 40, 40, "map1", DateTime.Now, DateTime.Now, "Dimensions: 40 by 40"),
            new Map(2, 20, 10, "map2", DateTime.Now, DateTime.Now, "Dimensions: 20 by 10"),
            new Map(3, 60, 60, "map3", DateTime.Now, DateTime.Now, "Dimensions: 60 by 60"),
            new Map(4, 50, 50, "map4", DateTime.Now, DateTime.Now, "Dimensions: 50 by 50")
        };

        // Endpoint to retrieve all maps
        [HttpGet("/maps")]
        public IEnumerable<Map> GetMapsArray()
        {
            return _maps;
        }

        // Endpoint to retrieve square maps
        [HttpGet("/maps/square")]
        public IEnumerable<Map> GetMapSquare()
        {
            return _maps.Where(x => x.Columns == x.Rows);
        }

        // Endpoint to retrieve a map by its ID
        [HttpGet("{id}", Name = "MapsRoute")]
        public IActionResult GetMapByID(int id)
        {
            if (id == null)
            {
                return BadRequest("ID should not be null");
            }
            if (id >= 1 && id <= _maps.Count)
            {
                var map = _maps.Where(x => x.ID == id);
                return Ok(map);
            }
            else
            {
                return BadRequest("ID must be within range");
            }
        }

        // Endpoint to add a new map
        [HttpPost()]
        public IActionResult PostMap(Map map)
        {
            try
            {
                if (map == null)
                {
                    return BadRequest("Map cannot be null");
                }
                if (_maps.Any(x => x.Name == map.Name))
                {
                    return BadRequest("A map with a similar name already exists");
                }
                int mapid = _maps.Count + 1;
                Console.WriteLine("ID is " + mapid);
                map.ID = mapid;
                map.CreateDate = DateTime.Now;
                map.ModifiedDate = DateTime.Now;
                _maps.Add(map);
                return CreatedAtRoute("MapsRoute", new { id = map.ID }, map);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Endpoint to update a map by its ID
        [HttpPut("{id}")]
        public IActionResult PutMap(int id, Map map)
        {
            if (id >= 1 && id <= _maps.Count)
            {
                _maps[id - 1].ID = id;
                _maps[id - 1].Columns = map.Columns;
                _maps[id - 1].Rows = map.Rows;
                _maps[id - 1].Name = map.Name;
                _maps[id - 1].ModifiedDate = DateTime.Now;
                _maps[id - 1].Description = map.Description;
                return NoContent();
            }
            else
            {
                return BadRequest("ID must be within range or cannot be null");
            }
        }

        // Endpoint to delete a map by its ID
        [HttpDelete("{id}")]
        public IActionResult DeleteMap(int id)
        {
            if (id <= 0 || id > _maps.Count)
            {
                return BadRequest("ID must be within range");
            }

            _maps.RemoveAt(id - 1);

            // Adjust IDs of remaining maps
            for (int i = id - 1; i < _maps.Count; i++)
            {
                _maps[i].ID = i + 1;
            }

            return Ok("Map successfully removed");
        }

        [HttpGet("{id}/{x}-{y}")]
        public IActionResult GetMap(int id, int x, int y)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be null or negative");
            }
            if (id > _maps.Count)
            {
                return NotFound("Map not found");
            }

            var map = _maps.FirstOrDefault(m => m.ID == id);
            if (map == null)
            {
                return NotFound("Map not found");
            }

            int rows = map.Rows;
            int columns = map.Columns;

            if (x < 0 || x >= rows || y < 0 || y >= columns)
            {
                return BadRequest("Coordinates are out of range");
            }

            // Assuming the coordinates are valid if they fall within the dimensions of the map
            return Ok(true);
        }
    }
}
