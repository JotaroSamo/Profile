using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Chat;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.GetMessages;

using Microsoft.Extensions.Logging;

public class GetMessagesInChatQueryHandler : IQueryHandler<GetMessagesInChatQuery, Result<BaseChat>>
{
    private readonly IChatService _chatService;
    private readonly ILogger<GetMessagesInChatQueryHandler> _logger;

    public GetMessagesInChatQueryHandler(IChatService chatService, ILogger<GetMessagesInChatQueryHandler> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    public async Task<Result<BaseChat>> Handle(GetMessagesInChatQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching messages for chat ID {ChatId}...", request.ChatId);

        var chatResult = await _chatService.GetChatMessage(request.ChatId);

        if (chatResult.IsFailure)
        {
            _logger.LogError("Failed to retrieve messages for chat ID {ChatId}: {Error}", request.ChatId, chatResult.Error);
            return Result.Failure<BaseChat>(chatResult.Error);
        }

        _logger.LogInformation("Successfully retrieved messages for chat ID {ChatId}.", request.ChatId);
        return Result.Success(chatResult.Value);
    }
}
