using MediatR;
using profile_API.HostedService.Notification.Command;
using profile_Application.Chat.GetUsersInChat;
using profile_Application.Chat.Query.CheckMessage;
using profile_MapperModel.Profile.Chat;

namespace profile_API.HostedService;

public class MessageCheckerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<MessageCheckerService> _logger;

    public MessageCheckerService(IServiceScopeFactory serviceScopeFactory, ILogger<MessageCheckerService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Service up at {Time}", DateTime.UtcNow);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var result = await mediator.Send(new CheckMessageQuery(DateTime.UtcNow));

                if (result.IsFailure)
                {
                    _logger.LogWarning("Check Failed: {Error}", result.Error);
                }
                else
                {
                    foreach (var message in result.Value)
                    {
                        var users = await mediator.Send(new GetUsersInChatQuery(message.ChatId));
                        if (users.IsSuccess)
                        {
                            foreach (var user in users.Value)
                            {
                                _logger.LogInformation("Processing message: {MessageId}", message.PublicId);

                                var send = await mediator.Send(new SendNotificationCommand(message, user.PublicId));
                                if (send.IsFailure)
                                {
                                    _logger.LogWarning("Send Failed for message {MessageId}: {Error}", message.PublicId, send.Error);
                                }
                                else
                                {
                                    _logger.LogInformation("Message {MessageId} sent successfully", message.PublicId);
                                }
                            }
                        }
                        else
                        {
                            _logger.LogWarning("Failed to get users for chat {ChatId}: {Error}", message.ChatId, users.Error);
                        }
                    }

                }
            }

            _logger.LogInformation("Service Sleep at {Time}", DateTime.UtcNow);

            await Task.Delay(10000, stoppingToken);
        }
    }
}
