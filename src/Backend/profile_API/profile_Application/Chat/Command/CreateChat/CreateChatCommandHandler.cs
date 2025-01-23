using Microsoft.Extensions.Logging;
using profile_Application.Chat.CreateChat;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Chat;
using profile_Domain.Exception;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.Command.CreateChat;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, BaseChat>
{
    private readonly IChatService _chatService;
    private readonly ILogger<CreateChatCommandHandler> _logger;

    public CreateChatCommandHandler(IChatService chatService, ILogger<CreateChatCommandHandler> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    public async Task<BaseChat> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating chat with title '{Title}' for users {UserIds}.", request.CreateChat.Title, string.Join(", ", request.CreateChat.UsersIds));

        // Создание чата с валидацией
        var createChatResult = profile_Domain.Chat.Chat.Create(request.CreateChat.Title, request.CreateChat.UsersIds);
        
        // Проверка на наличие ошибок при создании чата
        if (createChatResult.IsFailure)
        {
            _logger.LogWarning("Failed to create chat: {Error}", createChatResult.Error);
            throw new ProfileException(500, createChatResult.Error);
        }

        // Создание чата в сервисе
        var chatResult = await _chatService.CreateChat(createChatResult.Value);

        // Обработка ошибок сохранения чата
        if (chatResult.IsFailure)
        {
            _logger.LogError("Error saving chat: {Error}", chatResult.Error);
            throw new ProfileException(500, chatResult.Error);
        }

        _logger.LogInformation("Chat '{Title}' created successfully with ID {ChatId}.", request.CreateChat.Title, chatResult.Value.PublicId);
        
        // Возвращение успешно созданного чата
        return chatResult.Value;
    }
}

