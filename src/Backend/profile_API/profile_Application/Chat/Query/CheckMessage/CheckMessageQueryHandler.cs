using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Chat;

namespace profile_Application.Chat.Query.CheckMessage;

public class CheckMessageQueryHandler : IQueryHandler<CheckMessageQuery, Result<List<profile_MapperModel.Profile.Chat.CheckMessage>>>
{
    private readonly IMessageService _messageService;
    private readonly ILogger<CheckMessageQueryHandler> _logger;

    public CheckMessageQueryHandler(IMessageService messageService, ILogger<CheckMessageQueryHandler> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    public async Task<Result<List<profile_MapperModel.Profile.Chat.CheckMessage>>> Handle(CheckMessageQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CheckMessageQuery for time: {Time}", request.Now);

        var messages = await _messageService.CheckNewMessage(request.Now);
        if (messages.IsFailure)
        {
            _logger.LogError("Failed to check new messages: {Error}", messages.Error);
            return Result.Failure<List<profile_MapperModel.Profile.Chat.CheckMessage>>(messages.Error);
        }

        _logger.LogInformation("Successfully checked new messages. Count: {Count}", messages.Value.Count);
        return Result.Success<List<profile_MapperModel.Profile.Chat.CheckMessage>>(messages.Value);
    }
}
