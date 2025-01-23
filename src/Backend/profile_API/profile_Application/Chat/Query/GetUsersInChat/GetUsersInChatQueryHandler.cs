using profile_Application.Core.Queries.Contracts;
using profile_Core.Chat;
using profile_Domain.Exception;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.Query.GetUsersInChat;

public class GetUsersInChatQueryHandler: IQueryHandler<GetUsersInChatQuery, List<BaseUser>>
{
    private readonly IUserChatConnectionService _userChatConnectionService;

    public GetUsersInChatQueryHandler(IUserChatConnectionService userChatConnectionService)
    {
        _userChatConnectionService = userChatConnectionService;
    }
    public async Task<List<BaseUser>> Handle(GetUsersInChatQuery request, CancellationToken cancellationToken)
    {
        var users = await _userChatConnectionService.GetChatUsers(request.ChatId);
        if (users.IsFailure)
        {
            throw new ProfileException(500, users.Error);
        }
        return users.Value;

    }
}