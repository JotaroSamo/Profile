using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.CreateMessage;

public class CreateMessageCommand : ICommand<Result<BaseMessage>>
{
    public profile_MapperModel.Profile.Chat.CreateMessage CreateMessage { get; }

    public CreateMessageCommand(profile_MapperModel.Profile.Chat.CreateMessage createMessage)
    {
        CreateMessage = createMessage;
    }
}