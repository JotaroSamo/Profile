namespace profile_MapperModel.Profile.Chat;

public class CheckMessage
{
    public Guid PublicId { get; set; }
    
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Username { get; set; }
        
    public DateTime Timestamp { get; set; }
}