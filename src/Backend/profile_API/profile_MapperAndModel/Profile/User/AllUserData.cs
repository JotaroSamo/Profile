using profile_MapperModel.Profile.Chat;
using profile_MapperModel.Profile.Post;

namespace profile_MapperModel.Profile.User;

public class AllUserData
{
    public Guid PublicId { get; set; }
    public string Login { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl { get; set; }
    
    public ICollection<BaseChat> Chats { get; set; } = [];
    public ICollection<BasePost> Posts { get; set; } = [];

}