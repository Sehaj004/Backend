using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Persistence
{
    public class RobotCommandRepository : IRobotCommandDataAccess
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=YOUR_PASSWORD_GOES_HERE_SIT331;Database=sit331";

        public void DeleteRobotCommand(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM RobotCommands WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public List<RobotCommand> GetMoveCommands()
        {
            const string sqlCommand = "SELECT * FROM RobotCommands WHERE IsMoveCommand = true";
            return ExecuteReader<RobotCommand>(sqlCommand);
        }

        public RobotCommand GetRobotCommandById(int id)
        {
            const string sqlCommand = "SELECT * FROM RobotCommands WHERE ID = @id";
            var dbParams = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", id)
            };
            return ExecuteReader<RobotCommand>(sqlCommand, dbParams).FirstOrDefault();
        }

        public List<RobotCommand> GetRobotCommands()
        {
            const string sqlCommand = "SELECT * FROM RobotCommands";
            return ExecuteReader<RobotCommand>(sqlCommand);
        }

        public void InsertRobotCommand(RobotCommand newCommand)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "INSERT INTO RobotCommands (Name, IsMoveCommand, CreateDate, ModifiedDate, Description) " +
                "VALUES (@Name, @IsMoveCommand, @CreateDate, @ModifiedDate, @Description)", conn);
            cmd.Parameters.AddWithValue("@Name", newCommand.Name);
            cmd.Parameters.AddWithValue("@IsMoveCommand", newCommand.IsMoveCommand);
            cmd.Parameters.AddWithValue("@CreateDate", newCommand.CreateDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", newCommand.ModifiedDate);
            cmd.Parameters.AddWithValue("@Description", newCommand.Description);
            cmd.ExecuteNonQuery();
        }

        public void UpdateRobotCommand(int id, RobotCommand updatedCommand)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "UPDATE RobotCommands SET Name = @Name, IsMoveCommand = @IsMoveCommand, " +
                "CreateDate = @CreateDate, ModifiedDate = @ModifiedDate, Description = @Description " +
                "WHERE ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", updatedCommand.Name);
            cmd.Parameters.AddWithValue("@IsMoveCommand", updatedCommand.IsMoveCommand);
            cmd.Parameters.AddWithValue("@CreateDate", updatedCommand.CreateDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", updatedCommand.ModifiedDate);
            cmd.Parameters.AddWithValue("@Description", updatedCommand.Description);
            cmd.ExecuteNonQuery();
        }

        private List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[] dbParams = null) where T : class, new()
        {
            var entities = new List<T>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand(sqlCommand, conn);
            if (dbParams != null)
            {
                cmd.Parameters.AddRange(dbParams.Where(x => x.Value is not null).ToArray());
            }
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entity = new T();
                dr.MapTo(entity);
                entities.Add(entity);
            }
            return entities;
        }
    }
}
