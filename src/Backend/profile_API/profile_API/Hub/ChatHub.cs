using MediatR;
using Microsoft.AspNetCore.SignalR;
using profile_Application.Chat.CreateConnection;
using profile_Application.Chat.CreateMessage;
using profile_Application.Chat.DeleteConnection;
using profile_Application.Chat.GetMessages;
using profile_Core.Chat;
using profile_Core.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_API.Hub;

public class ChatHub: Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IMediator _mediator;
    private readonly IUserChatConnectionService _userChatConnectionService;

    public ChatHub(IMediator mediator,IUserChatConnectionService userChatConnectionService)
    {
        _mediator = mediator;
        _userChatConnectionService = userChatConnectionService;
    }
    public async override Task OnConnectedAsync()
    {
        Guid.TryParse(Context.GetHttpContext().Request.Query["chatId"], out Guid chatId);
        
        // Создание команды для создания соединения
        var command = new CreateConnectionCommand(chatId, Context.ConnectionId);
        var result = await _mediator.Send(command);

        // Проверка на наличие ошибок при создании соединения
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }

        // Вызываем базовый метод после успешного подключения
        await base.OnConnectedAsync();
    }
    public async override Task<Task> OnDisconnectedAsync(Exception exception)
    {
        var result = await _mediator.Send(new DeleteConnectionCommand(Context.ConnectionId));
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(CreateMessage message)
    {
        var result = await _mediator.Send(new CreateMessageCommand(message));
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }

        // Рассылаем сообщение всем участникам чата
        await Clients.Group(message.ChatId.ToString()).SendAsync("ReceiveMessage", result.Value);
    }

    // Метод для получения сообщений в чате
    public async Task GetMessagesInChat(Guid chatId)
    {
        var result = await _mediator.Send(new GetMessagesInChatQuery(chatId));
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }

        // Отправляем назад пользователю его сообщения
        await Clients.Caller.SendAsync("LoadMessages", result.Value);
    }
}