using MediatR;
using Microsoft.AspNetCore.SignalR;
using profile_Application.Chat.CreateConnection;
using profile_Application.Chat.CreateMessage;
using profile_Application.Chat.DeleteConnection;
using profile_Application.Chat.GetMessages;
using profile_Application.Chat.GetUsersInChat;
using profile_Core.Chat;
using profile_Core.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_API.Hub;

public class ChatHub: Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IMediator _mediator;


    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
      
    }
    
    public async Task JoinGroup(Guid chatId)
    {
        
        // Создание команды для создания соединения
        var command = new CreateConnectionCommand(chatId, Context.ConnectionId);
        var result = await _mediator.Send(command);

        // Проверка на наличие ошибок при создании соединения
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveGroup(Guid chatId, Guid userId)
    {
        var result = await _mediator.Send(new DeleteConnectionCommand(chatId:chatId,userId:userId));
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }
        await Groups.RemoveFromGroupAsync(result.Value, chatId.ToString());
        
    }

    public async Task SendNotificationToUser(Guid chatId, string notification)
    {
        var users = await _mediator.Send(new GetUsersInChatQuery(chatId:chatId));
        if (users.IsFailure)
        {
            throw new HubException(users.Error);
        }

        foreach (var user in users.Value)
        {
            await Clients.User(user.PublicId.ToString()).SendAsync(notification);
        }

       
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
        await SendNotificationToUser(message.ChatId, $"New Message from {result.Value.Username}");
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