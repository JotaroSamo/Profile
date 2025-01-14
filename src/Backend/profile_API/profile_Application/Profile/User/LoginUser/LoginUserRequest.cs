using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Model;

namespace profile_Application.Profile.User.LoginUser;

public class LoginUserRequest : ICommand<Result<JwtModel>>
{
    public profile_MapperModel.Profile.User.LoginUser LoginUser { get; }

    public LoginUserRequest(profile_MapperModel.Profile.User.LoginUser loginUser)
    {
        LoginUser = loginUser;
    }
}