using profile_DataAccess.Context.Entity.Profile;

namespace profile_DataAccess.Context.Entity.Chat;

public class MessageEntity
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity UserEntity { get; set; }
    
    public Guid ChatId { get; set; }
    
    public ChatEntity ChatEntity { get; set; }
    
    public DateTime Timestamp { get; set; }
}