using CSharpFunctionalExtensions;
using profile_Domain.Profile;

namespace profile_Domain.Chat;

public class Message
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Guid ChatId { get; set; }
    
    public Chat Chat { get; set; }
    
    public DateTime Timestamp { get; set; }

    private Message(string content, Guid chatId, Guid userId)
    {
        Content = content;
        ChatId = chatId;
        PublicId = Guid.NewGuid();
        UserId = userId;
        Timestamp = DateTime.UtcNow;
    }

    public static Result<Message> Create(string content, Guid chatId, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return Result.Failure<Message>("Message cannot be null or empty.");
        }
        if (chatId == Guid.Empty)
        {
            return Result.Failure<Message>("Chat ID cannot be empty.");
        }

        if (userId == Guid.Empty)
        {
               return Result.Failure<Message>("User ID cannot be empty."); 
        }
        var message = new Message(content, chatId, userId);
        return Result.Success<Message>(message);
    }
}