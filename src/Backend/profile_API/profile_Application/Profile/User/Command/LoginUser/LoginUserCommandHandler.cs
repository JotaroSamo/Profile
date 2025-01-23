using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.JWT;
using profile_Core.Model;
using profile_Core.Password;
using profile_Core.Profile;
using profile_Domain.Exception;

namespace profile_Application.Profile.User.LoginUser;

using Microsoft.Extensions.Logging;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, JwtModel>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<LoginUserCommandHandler> _logger;

    public LoginUserCommandHandler(IUserService userService, IJwtService jwtService, IPasswordService passwordService, ILogger<LoginUserCommandHandler> logger)
    {
        _userService = userService;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<JwtModel> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to login user {Login}", command.LoginUser.Login);

        var user = await _userService.GetUserByLogin(command.LoginUser.Login);
        if (user == null)
        {
            _logger.LogWarning("Login attempt failed: user not found for {Login}", command.LoginUser.Login);
            throw new ProfileException(404, $"Login attempt failed: user not found for {command.LoginUser.Login}");
        }

        var hashModel = new HashModel()
        {
            HashPassword = user.HasPassword,
            Salt = user.Salt,
        };

        if (!_passwordService.VerifyPassword(hashModel, command.LoginUser.Password))
        {
            _logger.LogWarning("Login attempt failed: invalid password for user {Login}", command.LoginUser.Login);
            throw new ProfileException(404, $"Login attempt failed: invalid password for user {command.LoginUser.Login}");
        }

        var token = _jwtService.GenerateJwtToken(user.PublicId, user.Login);
        _logger.LogInformation("User {Login} logged in successfully.", command.LoginUser.Login);
        return token;
    }
}
