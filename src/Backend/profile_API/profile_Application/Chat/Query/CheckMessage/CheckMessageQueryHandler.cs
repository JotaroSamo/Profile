using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Chat;
using profile_Domain.Exception;

namespace profile_Application.Chat.Query.CheckMessage;

public class CheckMessageQueryHandler : IQueryHandler<CheckMessageQuery, List<profile_MapperModel.Profile.Chat.CheckMessage>>
{
    private readonly IMessageService _messageService;
    private readonly ILogger<CheckMessageQueryHandler> _logger;

    public CheckMessageQueryHandler(IMessageService messageService, ILogger<CheckMessageQueryHandler> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    public async Task<List<profile_MapperModel.Profile.Chat.CheckMessage>> Handle(CheckMessageQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CheckMessageQuery for time: {Time}", request.Now);

        var messages = await _messageService.CheckNewMessage(request.Now);
        if (messages.IsFailure)
        {
            _logger.LogError("Failed to check new messages: {Error}", messages.Error);
            throw new ProfileException(500,messages.Error);
        }

        _logger.LogInformation("Successfully checked new messages. Count: {Count}", messages.Value.Count);
        return messages.Value;
    }
}
