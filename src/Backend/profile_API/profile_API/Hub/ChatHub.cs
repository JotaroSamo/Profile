using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using profile_Application.Chat.CreateConnection;
using profile_Application.Chat.CreateMessage;
using profile_Application.Chat.DeleteConnection;
using profile_Application.Chat.GetChats;
using profile_Application.Chat.GetMessages;
using profile_Application.Chat.GetUsersInChat;
using profile_Application.Profile.User.GetUserId;
using profile_Core.Chat;
using profile_Core.Contracts;
using profile_Domain.Notification;
using profile_MapperModel.Profile.Chat;

namespace profile_API.Hub;

public class ChatHub: Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IMediator _mediator;


    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
      
    }

    public async override Task OnConnectedAsync()
    {
        var userId = await _mediator.Send(new GetUserIdQuery());
        if (userId.IsFailure)
        {
            throw new HubException(userId.Error);
        }
        var chats = await _mediator.Send(new GetChatQuery(userId.Value));
        if (chats.IsFailure)
        {
            throw new HubException(chats.Error);
        }
       
        foreach (var chat in chats.Value.Chats)
        {
            // Создание команды для создания соединения
            var command = new CreateConnectionCommand(chat.PublicId, Context.ConnectionId);
            var result = await _mediator.Send(command);

            // Проверка на наличие ошибок при создании соединения
            if (result.IsFailure)
            {
                throw new HubException(result.Error);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, chat.PublicId.ToString());
        }
    }

    public async override Task<Task> OnDisconnectedAsync(Exception? exception)
    {
     
        var userId = await _mediator.Send(new GetUserIdQuery());
        if (userId.IsFailure)
        {
            throw new HubException(userId.Error);
        }
        
        var result = await _mediator.Send(new DeleteConnectionCommand(Context.ConnectionId));
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }

        if (!result.Value)
        {
            throw new HubException($"Unknown error {Context.ConnectionId}");
        }
        var chats = await _mediator.Send(new GetChatQuery(userId.Value));
        if (chats.IsFailure)
        {
            throw new HubException(chats.Error);
        }
        foreach (var chat in chats.Value.Chats)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.PublicId.ToString());    
        }
        return base.OnDisconnectedAsync(exception);
    }
    
    public async Task SendNotificationToUser(Guid chatId, Guid userId ,NotificationModel model)
    {
        var users = await _mediator.Send(new GetUsersInChatQuery(chatId:chatId));
        if (users.IsFailure)
        {
            throw new HubException(users.Error);
        }

        foreach (var user in users.Value)
        {
            if (user.PublicId != userId)
            {
                await Clients.User(user.PublicId.ToString()).SendAsync("ReceiveNotification",model);
            }
        }

       
    }
    [Authorize]
    public async Task SendMessage(CreateMessage message)
    {
        var result = await _mediator.Send(new CreateMessageCommand(message));
        if (result.IsFailure)
        {
            throw new HubException(result.Error);
        }
        // Рассылаем сообщение всем участникам чата
        await Clients.Group(message.ChatId.ToString()).SendAsync("ReceiveMessage", result.Value);
        await SendNotificationToUser(message.ChatId,  message.UserId,new NotificationModel()
        {
            Title = "New Message",
            UserName = result.Value.Username,
            Date = result.Value.Timestamp,
        });
    }

    [Authorize]
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