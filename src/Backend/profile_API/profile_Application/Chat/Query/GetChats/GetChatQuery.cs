using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.GetChats;

public class GetChatQuery : IQuery<Result<UserChats>>
{
    public Guid UserId { get; }

    public GetChatQuery(Guid userId)
    {
        UserId = userId;
    }
}