using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace robot_controller_api.Persistence
{
    public class RobotCommandADO : IRobotCommandDataAccess
    {
        // Connection string for the database
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=YOUR_PASSWORD_GOES_HERE_SIT331;Database=sit331";

        // Method to fetch all robot commands
        public List<RobotCommand> GetRobotCommands()
        {
            var robotCommands = new List<RobotCommand>();

            // Establish connection to the database
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            // SQL command to select all robot commands
            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn);

            // Execute the SQL command and read the results
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var name = dr.GetString(1);
                var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
                var isMoveCommand = dr.GetBoolean(3);
                var createDate = dr.GetDateTime(4);
                var modDate = dr.GetDateTime(5);

                var robotCommand = new RobotCommand(id, name, isMoveCommand, createDate, modDate, descr);
                robotCommands.Add(robotCommand);
            }

            return robotCommands;
        }

        // Method to fetch move commands only
        public List<RobotCommand> GetMoveCommands()
        {
            var robotCommands = new List<RobotCommand>();

            // Establish connection to the database
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            // SQL command to select move commands only
            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE ismovecommand = true", conn);

            // Execute the SQL command and read the results
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var name = dr.GetString(1);
                var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
                var isMoveCommand = dr.GetBoolean(3);
                var createDate = dr.GetDateTime(4);
                var modDate = dr.GetDateTime(5);

                var robotCommand = new RobotCommand(id, name, isMoveCommand, createDate, modDate, descr);
                robotCommands.Add(robotCommand);
            }

            return robotCommands;
        }

        // Method to add a new robot command
        public void InsertRobotCommand(RobotCommand newCommand)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "INSERT INTO robotcommand (\"Name\", description, ismovecommand, createddate, modifieddate) VALUES (@Name, @Description, @IsMoveCommand, @CreateDate, @ModifiedDate)";
            using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Name", newCommand.Name);
            cmd.Parameters.AddWithValue("@Description", newCommand.Description);
            cmd.Parameters.AddWithValue("@IsMoveCommand", newCommand.IsMoveCommand);
            cmd.Parameters.AddWithValue("@CreateDate", newCommand.CreateDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", newCommand.ModifiedDate);

            cmd.ExecuteNonQuery();
        }

        // Method to fetch a command by its ID
        public RobotCommand GetRobotCommandById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "SELECT * FROM robotcommand WHERE ID = @ID";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var ID = dr.GetInt32(0);
                var name = dr.GetString(1);
                var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
                var isMoveCommand = dr.GetBoolean(3);
                var createDate = dr.GetDateTime(4);
                var modDate = dr.GetDateTime(5);

                return new RobotCommand(ID, name, isMoveCommand, createDate, modDate, descr);
            }

            return null;
        }

        // Method to update a robot command
        public void UpdateRobotCommand(int id, RobotCommand updatedCommand)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "UPDATE robotcommand SET \"Name\" = @Name, description = @Description, ismovecommand = @IsMoveCommand WHERE id = @ID";
            using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", updatedCommand.Name);
            cmd.Parameters.AddWithValue("@Description", updatedCommand.Description);
            cmd.Parameters.AddWithValue("@IsMoveCommand", updatedCommand.IsMoveCommand);

            cmd.ExecuteNonQuery();
        }

        // Method to delete a robot command
        public void DeleteRobotCommand(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            string query = "DELETE FROM robotcommand WHERE id = @ID";
            using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
        }
    }
}
