using CSharpFunctionalExtensions;
using profile_MapperModel.Profile.Chat;
using profile_MapperModel.Profile.User;

namespace profile_Core.Chat;

public interface IChatService
{
    public Task<Result<BaseChat>> CreateChat(profile_Domain.Chat.Chat createChat);
    public Task<Result<BaseChat>> GetChatMessage(Guid chatId);
    
  
    
    
}