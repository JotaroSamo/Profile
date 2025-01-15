using CSharpFunctionalExtensions;
using profile_Domain.Chat;

namespace profile_Domain.Profile;

public class User
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Login { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl { get; set; }
    
    public byte[] HasPassword { get; set; }
    
    public byte[] Salt { get; set; }
    
    public ICollection<Chat.Chat> Chats { get; set; } = new List<Chat.Chat>();
    public ICollection<Post> Posts { get; set; } = [];
    
    public ICollection<UserChatConnection> Connections { get; set; } = [];

    public User()
    {
        
    }
    public User(string login, string firstName, string avatarUrl, string lastName, byte[] hasPassword, byte[] salt)
    {
        PublicId = Guid.NewGuid();
        Login = login;
        AvatarUrl = avatarUrl;
        FirstName = firstName;
        LastName = lastName;
        HasPassword = hasPassword;
        Salt = salt;
        
    }

    public static Result<User> Create(string login, string firstName,string avatarUrl ,string lastName, byte[] hasPassword, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<User>("Invalid login or password");
        }
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<User>("Invalid first name");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<User>("Invalid last name");
        }
        if (hasPassword == null || hasPassword.Length == 0 || salt == null || salt.Length == 0)
        {
            return Result.Failure<User>("Invalid password");
        }
        var user = new User(login, firstName, avatarUrl, lastName, hasPassword, salt);
        return Result.Success(user);
    }

}