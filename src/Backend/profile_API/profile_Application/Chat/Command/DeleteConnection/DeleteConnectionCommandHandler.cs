using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;
using profile_Core.Contracts;

namespace profile_Application.Chat.DeleteConnection;

using Microsoft.Extensions.Logging;

public class DeleteConnectionCommandHandler : ICommandHandler<DeleteConnectionCommand,Result<bool>>
{
    private readonly IUserChatConnectionService _userChatConnectionService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<DeleteConnectionCommandHandler> _logger;

    public DeleteConnectionCommandHandler(IUserChatConnectionService userChatConnectionService,IHttpContextService httpContextService ,ILogger<DeleteConnectionCommandHandler> logger)
    {
        _userChatConnectionService = userChatConnectionService;
        _httpContextService = httpContextService;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Handling DeleteConnectionCommand {requestConnectionId}", request.ConnectionId);

        var success = await _userChatConnectionService.DeleteConnection(request.ConnectionId);

        if (success.IsFailure)
        {
            _logger.LogWarning("Failed to delete connection {requestConnectionId}", request.ConnectionId);
            return Result.Failure<bool>("Failed to delete connection");
        }

        _logger.LogInformation("Successfully deleted connection {requestConnectionId}", request.ConnectionId);
        return success;
    }
}
