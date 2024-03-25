namespace robot_controller_api.Persistence
{
    public interface IRobotCommandDataAccess
    {
        void DeleteRobotCommand(int id);
        List<RobotCommand> GetMoveCommands();
        RobotCommand GetRobotCommandById(int id);
        List<RobotCommand> GetRobotCommands();
        void InsertRobotCommand(RobotCommand newCommand);
        void UpdateRobotCommand(int id, RobotCommand updatedCommand);
    }
}