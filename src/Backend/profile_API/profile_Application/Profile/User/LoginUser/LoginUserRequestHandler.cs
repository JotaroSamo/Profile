using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.JWT;
using profile_Core.Model;
using profile_Core.Password;
using profile_Core.Profile;

namespace profile_Application.Profile.User.LoginUser;

using Microsoft.Extensions.Logging;

public class LoginUserRequestHandler : ICommandHandler<LoginUserRequest, Result<JwtModel>>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<LoginUserRequestHandler> _logger;

    public LoginUserRequestHandler(IUserService userService, IJwtService jwtService, IPasswordService passwordService, ILogger<LoginUserRequestHandler> logger)
    {
        _userService = userService;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<Result<JwtModel>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to login user {Login}", request.LoginUser.Login);

        var user = await _userService.GetUserByLogin(request.LoginUser.Login);
        if (user == null)
        {
            _logger.LogWarning("Login attempt failed: user not found for {Login}", request.LoginUser.Login);
            return Result.Failure<JwtModel>("Invalid username or password");
        }

        var hashModel = new HashModel()
        {
            HashPassword = user.HasPassword,
            Salt = user.Salt,
        };

        if (!_passwordService.VerifyPassword(hashModel, request.LoginUser.Password))
        {
            _logger.LogWarning("Login attempt failed: invalid password for user {Login}", request.LoginUser.Login);
            return Result.Failure<JwtModel>("Invalid username or password");
        }

        var token = _jwtService.GenerateJwtToken(user.PublicId, user.Login);
        _logger.LogInformation("User {Login} logged in successfully.", request.LoginUser.Login);
        return Result.Success(token);
    }
}
