using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.GetMessages;

public class GetMessagesInChatQuery : IQuery<Result<BaseChat>>
{
    public Guid ChatId { get; }

    public GetMessagesInChatQuery(Guid chatId)
    {
        ChatId = chatId;
    }
}