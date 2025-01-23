using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Profile;
using profile_Domain.Exception;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.GetChats;

using Microsoft.Extensions.Logging;

public class GetChatQueryHandler : IQueryHandler<GetChatQuery, UserChats>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetChatQueryHandler> _logger;

    public GetChatQueryHandler(IUserService userService, ILogger<GetChatQueryHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<UserChats> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching chat data for user ID {UserId}...", request.UserId);

        var userChats = await _userService.GetUserChatsByPublicId(request.UserId);

        if (userChats.IsFailure)
        {
            _logger.LogError("Failed to retrieve chat data for user ID {UserId}: {Error}", request.UserId, userChats.Error);
            throw new ProfileException(500, userChats.Error);
        }

        _logger.LogInformation("Successfully retrieved chat data for user ID {UserId}.", request.UserId);
        return userChats.Value;
    }
}
