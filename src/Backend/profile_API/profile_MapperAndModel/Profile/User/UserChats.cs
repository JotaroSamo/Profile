using profile_MapperModel.Profile.Chat;

namespace profile_MapperModel.Profile.User;

public class UserChats
{
    public Guid PublicId { get; set; }
    public string Login { get; set; } = string.Empty;
    public List<ChatEmty> Chats { get; set; } = new List<ChatEmty>();

}