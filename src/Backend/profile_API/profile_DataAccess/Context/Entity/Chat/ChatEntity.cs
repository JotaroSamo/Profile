using profile_DataAccess.Context.Entity.Profile;
using profile_Domain.Chat;

namespace profile_DataAccess.Context.Entity.Chat;

public class ChatEntity
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Title { get; set; }
    public ICollection<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}