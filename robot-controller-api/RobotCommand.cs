namespace robot_controller_api;

// A class representing a command for the robot in the robot controller API
public class RobotCommand
{
    // Unique identifier for the command
    public int ID { get; set; }
    
    // Name of the command
    public string Name { get; set; }
    
    // Indicates whether the command is a move command or not
    public bool IsMoveCommand { get; set; }
    
    // Date and time when the command was created
    public DateTime CreateDate { get; set; }
    
    // Date and time when the command was last modified
    public DateTime ModifiedDate { get; set; }
    
    // Optional description for the command
    public string? Description { get; set; }

    // Constructor for initializing a RobotCommand object
    public RobotCommand(
        int id, string name, bool isMoveCommand, DateTime createDate, DateTime modifiedDate,
        string? description)
    {
        ID = id;
        Name = name;
        IsMoveCommand = isMoveCommand;
        CreateDate = createDate;
        ModifiedDate = modifiedDate;
        Description = description;
    }

    public RobotCommand(){
        
    }
}
