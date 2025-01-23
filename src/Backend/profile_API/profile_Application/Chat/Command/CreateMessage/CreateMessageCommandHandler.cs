using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;
using profile_Domain.Chat;
using profile_Domain.Exception;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.Command.CreateMessage;

public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, BaseMessage>
{
    private readonly IMessageService _messageService;
    private readonly ILogger<CreateMessageCommandHandler> _logger;

    public CreateMessageCommandHandler(IMessageService messageService, ILogger<CreateMessageCommandHandler> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    public async Task<BaseMessage> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        // Создание сообщения с валидацией
        _logger.LogInformation("Creating message in chat {ChatId} from user {UserId}.", request.CreateMessage.ChatId, request.CreateMessage.UserId);

        var createdMessageResult = Message.Create(request.CreateMessage.Content, request.CreateMessage.ChatId, request.CreateMessage.UserId);

        // Проверка на наличие ошибок при создании сообщения
        if (createdMessageResult.IsFailure)
        {
            _logger.LogWarning("Message creation failed: {Error}", createdMessageResult.Error);
            throw new ProfileException(400,createdMessageResult.Error);
        }

        // Создание сообщения в сервисе
        var message = await _messageService.CreateMessage(createdMessageResult.Value);

        // Обработка ошибок сохранения сообщения
        if (message.IsFailure)
        {
            _logger.LogError("Failed to save message: {Error}", message.Error);
            throw new ProfileException(500,message.Error);
        }


        // Возвращение успешно созданного сообщения
        _logger.LogInformation("Message created successfully with ID: {MessageId}.", message.Value.PublicId);
        return message.Value;
    }

    
}

