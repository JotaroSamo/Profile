using profile_DataAccess.Context.Entity.Chat;

namespace profile_DataAccess.Context.Entity.Profile;

public class UserEntity
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Login { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl { get; set; }
    
    public byte[] HasPassword { get; set; }
    
    public byte[] Salt { get; set; }

    public ICollection<ChatEntity> Chats { get; set; } = [];
    public ICollection<PostEntity> Posts { get; set; } = [];
    
    public ICollection<UserChatConnectionEntity> Connections = [];

}