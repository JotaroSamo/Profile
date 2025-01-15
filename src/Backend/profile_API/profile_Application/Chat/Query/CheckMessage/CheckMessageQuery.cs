using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;

namespace profile_Application.Chat.Query.CheckMessage;

public class CheckMessageQuery : IQuery<Result<List<profile_MapperModel.Profile.Chat.CheckMessage>>>
{
    public DateTime Now { get; }

    public CheckMessageQuery(DateTime now)
    {
        Now = now;
    }
}