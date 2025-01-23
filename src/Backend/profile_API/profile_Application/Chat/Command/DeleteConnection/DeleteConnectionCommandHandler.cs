using CSharpFunctionalExtensions;
using profile_Application.Chat.Command.DeleteConnection;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;
using profile_Core.Contracts;
using profile_Domain.Exception;

namespace profile_Application.Chat.DeleteConnection;

using Microsoft.Extensions.Logging;

public class DeleteConnectionCommandHandler : ICommandHandler<DeleteConnectionCommand,bool>
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

    public async Task<bool> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Handling DeleteConnectionCommand {requestConnectionId}", request.ConnectionId);

        var success = await _userChatConnectionService.DeleteConnection(request.ConnectionId);

        if (success.IsFailure)
        {
            _logger.LogWarning("Failed to delete connection {requestConnectionId}", request.ConnectionId);
            throw new ProfileException(500,"Failed to delete connection");
        }

        _logger.LogInformation("Successfully deleted connection {requestConnectionId}", request.ConnectionId);
        return success.Value;
    }
}
