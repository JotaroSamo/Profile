using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;
using profile_API.Hub;
using profile_Application.Core.Commands.Contracts;
using profile_Domain.Notification;

namespace profile_API.HostedService.Notification.Command;

public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand, Result>
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly ILogger<SendNotificationCommandHandler> _logger;

    public SendNotificationCommandHandler(IHubContext<ChatHub> hubContext, ILogger<SendNotificationCommandHandler> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<Result> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = new NotificationModel()
        {
            Title = "New Message",
            UserName = request.Message.Username,
            Date = request.Message.Timestamp,
        };

        try
        {
            _logger.LogInformation("Sending notification to user {UserId} for message {MessageId}", request.Message.UserId, request.Message.PublicId);

            await _hubContext.Clients.User(request.Message.UserId.ToString()).SendAsync("ReceiveNotification", model);
            
            _logger.LogInformation("Notification successfully sent to user {UserId}", request.Message.UserId);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending notification to user {UserId}", request.Message.UserId);
            return Result.Failure("Error sending notification");
        }
    }
}
