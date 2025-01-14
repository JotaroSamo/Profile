using CSharpFunctionalExtensions;
using profile_MapperModel.Profile.Chat;

namespace profile_Core.Chat;

public interface IChatService
{
    public Task<Result<BaseChat>> CreateChat(profile_Domain.Chat.Chat createChat);
    public Task<Result<BaseChat>> GetChatMessage(Guid chatId);
    
    
}