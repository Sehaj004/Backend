namespace robot_controller_api.Persistence
{
    public interface IMapDataAccess
    {
        void DeleteMap(int id);
        Map GetMapById(int id);
        List<Map> GetRobotMap();
        List<Map> GetRobotMapSquare();
        bool IsMapIndexValid(int id, int x, int y);
        void PostMap(Map newmap);
        void UpdateMap(int id, Map newmap);
    }
}