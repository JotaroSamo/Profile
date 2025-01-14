using CSharpFunctionalExtensions;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;

namespace profile_Core.Chat;

public interface IMessageService
{
    public Task<Result<BaseMessage>> CreateMessage(Message message);
}