using profile_MapperModel.Profile.User;

namespace profile_MapperModel.Profile.Chat;

public class BaseChat
{
    public Guid PublicId { get; set; }
    
    public string Title { get; set; }
    public ICollection<BaseUser> Users { get; set; } = new List<BaseUser>();
    public ICollection<BaseMessage> Messages { get; set; } = new List<BaseMessage>();
}