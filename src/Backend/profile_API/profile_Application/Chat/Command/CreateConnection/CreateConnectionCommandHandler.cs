using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;
using profile_Core.Contracts;
using profile_Domain.Chat;
using profile_Domain.Exception;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.CreateConnection;

using Microsoft.Extensions.Logging;

public class CreateConnectionCommandHandler : ICommandHandler<CreateConnectionCommand, BaseUserChatConnection>
{
    private readonly IUserChatConnectionService _userChatConnectionService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<CreateConnectionCommandHandler> _logger;

    public CreateConnectionCommandHandler(IUserChatConnectionService userChatConnectionService, IHttpContextService httpContextService, ILogger<CreateConnectionCommandHandler> logger)
    {
        _userChatConnectionService = userChatConnectionService;
        _httpContextService = httpContextService;
        _logger = logger;
    }

    public async Task<BaseUserChatConnection> Handle(CreateConnectionCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserGuid().Value;

        // Проверка на существование пользователя
        if (userId == Guid.Empty)
        {
            _logger.LogWarning("User not found when trying to create connection.");
            throw new ProfileException(404, "User not found");
        }

        _logger.LogInformation("Creating connection for User ID: {UserId} with Connection ID: {ConnectionId} and Chat ID: {ChatId}.", userId, request.ConnectionId, request.ChatId);
        
        var connection = UserChatConnection.Create(request.ConnectionId, userId, request.ChatId);

        // Проверка на ошибки при создании подключения
        if (connection.IsFailure)
        {
            _logger.LogWarning("Invalid connection attempt: {Error}", connection.Error);
            throw new ProfileException(500,"Invalid connection");
        }

        var connect = await _userChatConnectionService.CreateConnection(connection.Value);
        
        // Обработка ошибок при создании подключения
        if (connect.IsFailure)
        {
            _logger.LogError("Failed to save connection: {Error}", connect.Error);
            throw new ProfileException(500,"Failed to create connection");
        }

        _logger.LogInformation("Successfully created a connection with ID: {ConnectionId}.", connect.Value);
        return connect.Value;
    }
}
