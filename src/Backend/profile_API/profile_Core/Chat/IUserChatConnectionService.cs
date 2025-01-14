using CSharpFunctionalExtensions;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;

namespace profile_Core.Chat;

public interface IUserChatConnectionService
{
    public Task<Result<BaseUserChatConnection>> CreateConnection(UserChatConnection userChatConnection);
    public Task<bool> DeleteConnection(string connectionId);
    
    public Task<Result<List<BaseUserChatConnection>>> GetConnections(Guid chatId);
    
}