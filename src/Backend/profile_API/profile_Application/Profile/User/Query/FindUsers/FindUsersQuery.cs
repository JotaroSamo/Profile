using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.Query.FindUsers;

public class FindUsersQuery : IQuery<Result<List<BaseUser>>>
{
    public string Login { get; }

    public FindUsersQuery(string login)
    {
        Login = login;
    }
}