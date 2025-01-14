using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;

namespace profile_Application.Chat.DeleteConnection;

using Microsoft.Extensions.Logging;

public class DeleteConnectionCommandHandler : ICommandHandler<DeleteConnectionCommand,Result<string>>
{
    private readonly IUserChatConnectionService _userChatConnectionService;
    private readonly ILogger<DeleteConnectionCommandHandler> _logger;

    public DeleteConnectionCommandHandler(IUserChatConnectionService userChatConnectionService, ILogger<DeleteConnectionCommandHandler> logger)
    {
        _userChatConnectionService = userChatConnectionService;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete connection with UserID: {UserID}.", request.UserId);

        var success = await _userChatConnectionService.DeleteConnection(request.ChatId,request.UserId);

        if (success.IsFailure)
        {
            _logger.LogWarning("Failed to delete connection with UserID: {UserID}. Connection may not exist.", request.UserId);
            return Result.Failure<string>($"Failed to delete connection with UserID: {request.UserId}");
        }

        _logger.LogInformation("Successfully deleted connection with UserID: {UserID}.", request.UserId);
        return success;
    }
}
