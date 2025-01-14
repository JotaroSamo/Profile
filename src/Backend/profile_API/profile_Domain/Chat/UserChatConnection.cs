using CSharpFunctionalExtensions;
using profile_Domain.Profile;

namespace profile_Domain.Chat;

public class UserChatConnection
{
    public long Id { get; set; }
    
    public string ConnectionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    public Guid ChatId { get; set; }
    
    public Chat Chat { get; set; } = null!;

    private UserChatConnection(string connectionId, Guid userId, Guid chatId)
    {
        ConnectionId = connectionId;
        UserId = userId;
    }

    public static Result<UserChatConnection> Create(string connectionId, Guid userId, Guid chatId)
    {
        if (string.IsNullOrWhiteSpace(connectionId))
        {
            return Result.Failure<UserChatConnection>("Invalid connection ID.");
        }

        if (userId == Guid.Empty)
        {
            return Result.Failure<UserChatConnection>("Invalid user ID.");
        }

        if (chatId == Guid.Empty)
        {
            return Result.Failure<UserChatConnection>("Invalid chatID.");
        }
        
        var userChatConnecton = new UserChatConnection(connectionId, userId, chatId);
        return Result.Success(userChatConnecton);
    }
}