using profile_MapperModel.Profile.Post;

namespace profile_MapperModel.Profile.User;

public class UserPosts
{
    public Guid PublicId { get; set; }
    public string Login { get; set; } = string.Empty;
    
    public List<BasePost> Posts { get; set; } = new List<BasePost>();
    
}