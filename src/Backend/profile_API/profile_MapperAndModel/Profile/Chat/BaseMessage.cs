namespace profile_MapperModel.Profile.Chat;

public class BaseMessage
{
    public Guid PublicId { get; set; }
    
    public string Content { get; set; }
    
    public string Username { get; set; }
        
    public DateTime Timestamp { get; set; }
}