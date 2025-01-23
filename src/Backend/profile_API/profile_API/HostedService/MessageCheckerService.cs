using MediatR;
using profile_API.HostedService.Notification.Command;
using profile_Application.Chat.Query.CheckMessage;
using profile_Application.Chat.Query.GetUsersInChat;
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
                
                    foreach (var message in result)
                    {
                        var users = await mediator.Send(new GetUsersInChatQuery(message.ChatId));

                            foreach (var user in users)
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

                
            }

            _logger.LogInformation("Service Sleep at {Time}", DateTime.UtcNow);

            await Task.Delay(10000, stoppingToken);
        }
    }
}
