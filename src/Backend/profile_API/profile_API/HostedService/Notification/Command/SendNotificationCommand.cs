using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_API.HostedService.Notification.Command;

public class SendNotificationCommand : ICommand<Result>
{
    public CheckMessage Message { get; }


    public SendNotificationCommand(CheckMessage message)
    {
        Message = message;
    }
}