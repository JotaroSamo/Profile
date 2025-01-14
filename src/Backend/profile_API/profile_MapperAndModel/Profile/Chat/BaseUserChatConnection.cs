namespace profile_MapperModel.Profile.Chat;

public class BaseUserChatConnection
{
    public string ConnectionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid ChatId { get; set; }
    
    public string UserName { get; set; } = string.Empty;
}