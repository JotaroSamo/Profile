using CSharpFunctionalExtensions;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;
using profile_MapperModel.Profile.User;

namespace profile_Core.Chat;

public interface IUserChatConnectionService
{
    public Task<Result<BaseUserChatConnection>> CreateConnection(UserChatConnection userChatConnection);
    public Task<Result<bool>> DeleteConnection(string conectionId);
    
    public Task<Result<List<BaseUserChatConnection>>> GetConnections(Guid chatId);
    
    public Task<Result<List<BaseUser>>> GetChatUsers(Guid chatId);
    
}