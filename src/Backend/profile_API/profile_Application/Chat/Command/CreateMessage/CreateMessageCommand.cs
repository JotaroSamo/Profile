using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.Command.CreateMessage;

public class CreateMessageCommand : ICommand<BaseMessage>
{
    public profile_MapperModel.Profile.Chat.CreateMessage CreateMessage { get; }

    public CreateMessageCommand(profile_MapperModel.Profile.Chat.CreateMessage createMessage)
    {
        CreateMessage = createMessage;
    }
}