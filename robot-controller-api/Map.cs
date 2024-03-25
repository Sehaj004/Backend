namespace robot_controller_api;

// A class representing a map in the robot controller API
public class Map
{
    // Unique identifier for the map
    public int ID { get; set; }
    
    // Number of rows in the map grid
    public int Rows {  get; set; }
    
    // Number of columns in the map grid
    public int Columns { get; set; }
    
    // Name of the map
    public string Name { get; set; }
    
    // Date and time when the map was created
    public DateTime CreateDate { get; set; }
    
    // Date and time when the map was last modified
    public DateTime ModifiedDate { get; set; }
    
    // Optional description for the map
    public string? Description { get; set; }
    
    // Constructor for initializing a Map object
    public Map(int id, int rows, int columns, string name, DateTime createDate, DateTime modifiedDate, string? description)
    {
        ID = id;
        Rows = rows;
        Columns = columns;
        Name = name;
        CreateDate = createDate;
        ModifiedDate = modifiedDate;
        Description = description;
    }
}
