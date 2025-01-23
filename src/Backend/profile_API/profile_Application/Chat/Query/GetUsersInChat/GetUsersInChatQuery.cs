using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.Query.GetUsersInChat;

public class GetUsersInChatQuery :IQuery<List<BaseUser>>
{
    public Guid ChatId { get; }

    public GetUsersInChatQuery(Guid chatId)
    {
        ChatId = chatId;
    }
}