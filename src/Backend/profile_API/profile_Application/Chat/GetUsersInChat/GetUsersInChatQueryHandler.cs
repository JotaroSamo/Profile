using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Chat;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.GetUsersInChat;

public class GetUsersInChatQueryHandler: IQueryHandler<GetUsersInChatQuery, Result<List<BaseUser>>>
{
    private readonly IUserChatConnectionService _userChatConnectionService;

    public GetUsersInChatQueryHandler(IUserChatConnectionService userChatConnectionService)
    {
        _userChatConnectionService = userChatConnectionService;
    }
    public async Task<Result<List<BaseUser>>> Handle(GetUsersInChatQuery request, CancellationToken cancellationToken)
    {
        var users = await _userChatConnectionService.GetChatUsers(request.ChatId);
        return users;

    }
}