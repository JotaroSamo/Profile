using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.Query.FindUsers;

public class FindUsersQuery : IQuery<List<BaseUser>>
{
    public string Query { get; }

    public FindUsersQuery(string query)
    {
        Query = query;
    }
}