using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;

namespace profile_Application.Chat.DeleteConnection;

public class DeleteConnectionCommand : ICommand<Result<string>>
{
    public Guid ChatId { get; }
    public Guid UserId { get; }

    public DeleteConnectionCommand(Guid chatId, Guid userId)
    {
        ChatId = chatId;
        UserId = userId;
    }
}