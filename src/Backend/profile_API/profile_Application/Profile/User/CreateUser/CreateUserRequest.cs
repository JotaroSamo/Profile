
using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.CreateUser;

public class CreateUserRequest : ICommand<Result<BaseUser>>
{
    public CreateUserRequest(profile_MapperModel.Profile.User.CreateUser createUser)
    {
        user = createUser;
    }

    public profile_MapperModel.Profile.User.CreateUser user {get;}
}