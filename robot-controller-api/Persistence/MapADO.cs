using Npgsql;
using robot_controller_api.Controllers;
using System;
using System.Collections.Generic;

namespace robot_controller_api.Persistence
{
    public class MapADO : IMapDataAccess
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=YOUR_PASSWORD_GOES_HERE_SIT331;Database=sit331";

        // Method to fetch all robot maps
        public List<Map> GetRobotMap()
        {
            var newmap = new List<Map>();

            // Establish connection to the database
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            // SQL command to select all robot maps
            using var cmd = new NpgsqlCommand("SELECT * FROM map", conn);

            // Execute the SQL command and read the results
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var name = dr.GetString(1);
                var rows = dr.GetInt32(2);
                var columns = dr.GetInt32(3);
                var descr = dr.IsDBNull(5) ? null : dr.GetString(5);
                var createDate = dr.GetDateTime(6);
                var modDate = dr.GetDateTime(7);

                var map = new Map(id, rows, columns, name, createDate, modDate, descr);
                newmap.Add(map);
            }

            return newmap;
        }

        // Method to fetch square robot maps
        public List<Map> GetRobotMapSquare()
        {
            var newmap = new List<Map>();

            // Establish connection to the database
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            // SQL command to select square robot maps
            using var cmd = new NpgsqlCommand("SELECT * FROM map WHERE rows = columns", conn);

            // Execute the SQL command and read the results
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var name = dr.GetString(1);
                var rows = dr.GetInt32(2);
                var columns = dr.GetInt32(3);
                var descr = dr.IsDBNull(5) ? null : dr.GetString(5);
                var createDate = dr.GetDateTime(6);
                var modDate = dr.GetDateTime(7);

                var map = new Map(id, rows, columns, name, createDate, modDate, descr);
                newmap.Add(map);
            }

            return newmap;
        }

        // Method to fetch a map by its ID
        public Map GetMapById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "SELECT * FROM map WHERE ID = @ID";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var ID = dr.GetInt32(0);
                var name = dr.GetString(1);
                var rows = dr.GetInt32(2);
                var columns = dr.GetInt32(3);
                var descr = dr.IsDBNull(5) ? null : dr.GetString(5);
                var createDate = dr.GetDateTime(6);
                var modDate = dr.GetDateTime(7);

                return new Map(ID, rows, columns, name, createDate, modDate, descr);
            }

            return null;
        }

        // Method to add a new map
        public void PostMap(Map newmap)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "INSERT INTO map (\"Name\", rows, columns, description, createddate, modifieddate) VALUES (@Name, @Rows, @Columns, @Description, @CreatedDate, @ModifiedDate)";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", newmap.Name);
            cmd.Parameters.AddWithValue("@Rows", newmap.Rows);
            cmd.Parameters.AddWithValue("@Columns", newmap.Columns);
            cmd.Parameters.AddWithValue("@Description", newmap.Description);
            cmd.Parameters.AddWithValue("@CreatedDate", newmap.CreateDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", newmap.ModifiedDate);

            cmd.ExecuteNonQuery();
        }

        // Method to update a map
        public void UpdateMap(int id, Map newmap)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "UPDATE map SET \"Name\" = @Name, rows = @Rows, columns = @Columns, description = @Description WHERE id = @ID";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", newmap.Name);
            cmd.Parameters.AddWithValue("@Rows", newmap.Rows);
            cmd.Parameters.AddWithValue("@Columns", newmap.Columns);
            cmd.Parameters.AddWithValue("@Description", newmap.Description);

            cmd.ExecuteNonQuery();
        }

        // Method to delete a map
        public void DeleteMap(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "DELETE FROM map WHERE id = @ID";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
        }

        // Method to check if map index is valid
        public bool IsMapIndexValid(int id, int x, int y)
        {
            int col = 0;
            int row = 0;

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "SELECT columns, rows FROM map WHERE id = @ID";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                col = dr.GetInt32(0);
                row = dr.GetInt32(1);
            }

            bool isValidIndex = x < col && y < row;
            return isValidIndex;
        }
    }
}
