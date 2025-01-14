using profile_DataAccess.Context.Entity.Profile;

namespace profile_DataAccess.Context.Entity.Chat;

public class UserChatConnectionEntity
{
    public long Id { get; set; }
    
    public string ConnectionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; } = null!;
    
    public Guid ChatId { get; set; }
    
    public ChatEntity Chat { get; set; } = null!;

    
}