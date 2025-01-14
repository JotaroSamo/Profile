using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;

namespace profile_Application.Chat.DeleteConnection;

using Microsoft.Extensions.Logging;

public class DeleteConnectionCommandHandler : ICommandHandler<DeleteConnectionCommand, bool>
{
    private readonly IUserChatConnectionService _userChatConnectionService;
    private readonly ILogger<DeleteConnectionCommandHandler> _logger;

    public DeleteConnectionCommandHandler(IUserChatConnectionService userChatConnectionService, ILogger<DeleteConnectionCommandHandler> logger)
    {
        _userChatConnectionService = userChatConnectionService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete connection with ID: {ConnectionId}.", request.ConnectionId);

        var success = await _userChatConnectionService.DeleteConnection(request.ConnectionId);

        if (!success)
        {
            _logger.LogWarning("Failed to delete connection with ID: {ConnectionId}. Connection may not exist.", request.ConnectionId);
            return false;
        }

        _logger.LogInformation("Successfully deleted connection with ID: {ConnectionId}.", request.ConnectionId);
        return true;
    }
}
