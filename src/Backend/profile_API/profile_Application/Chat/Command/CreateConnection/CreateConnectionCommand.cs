using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.CreateConnection;

public class CreateConnectionCommand : ICommand<BaseUserChatConnection>
{
    public Guid ChatId { get; }
    public string ConnectionId { get; }

    public CreateConnectionCommand(Guid chatId,string connectionId)
    {
        ChatId = chatId;
        ConnectionId = connectionId;
    }
}