using CSharpFunctionalExtensions;
using profile_Domain.Profile;

namespace profile_Domain.Chat;

public class Chat
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Title { get; set; }
    
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    
    public ICollection<User> Users { get; set; } = new List<User>();

    private Chat(string title, List<Guid> users)
    {
        Title = title;
        PublicId = Guid.NewGuid();
        
        // Пример создания пользователей на основе переданных идентификаторов
        foreach (var userId in users)
        {
            Users.Add(new User { PublicId = userId }); // Предполагаем, что у класса User есть свойство PublicId
        }
    }

    public static Result<Chat> Create(string title, List<Guid> users )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Chat>("Title cannot be null or empty.");
        }

        if (users == null || users.Count == 0)
        {
            return Result.Failure<Chat>("Users cannot be null or empty.");
        }

        var chat = new Chat(title, users);
        return Result.Success(chat);
    }
}