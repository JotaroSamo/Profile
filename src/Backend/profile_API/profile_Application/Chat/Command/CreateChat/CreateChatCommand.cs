using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_Application.Chat.CreateChat;

public class CreateChatCommand : ICommand<Result<BaseChat>>
{
    public profile_MapperModel.Profile.Chat.CreateChat CreateChat { get; }

    public CreateChatCommand(profile_MapperModel.Profile.Chat.CreateChat createChat)
    {
        CreateChat = createChat;
    }
}