using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Password;
using profile_Core.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.CreateUser;

using Microsoft.Extensions.Logging;

public class CreateUserRequestCommand : ICommandHandler<CreateUserCommand, Result<BaseUser>>
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<CreateUserRequestCommand> _logger;

    public CreateUserRequestCommand(IUserService userService, IPasswordService passwordService, ILogger<CreateUserRequestCommand> logger)
    {
        _userService = userService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<Result<BaseUser>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateUserRequest for user {Login}", command.user.Login);
        
        // Генерация соли и хеширование пароля
        var salt = _passwordService.GenerateSalt();
        var hasModel = _passwordService.HashPassword(command.user.Password, salt);
        
        var user = profile_Domain.Profile.User.Create(
            command.user.Login,
            command.user.FirstName,
            command.user.AvatarUrl,
            command.user.LastName,
            hasModel.HashPassword,
            hasModel.Salt);
        
        if (user.IsFailure)
        {
            _logger.LogError("Failed to create user: {Error}", user.Error);
            return Result.Failure<BaseUser>(user.Error);
        }

        var baseUser = await _userService.CreateUser(user.Value);
        if (baseUser.IsFailure)
        {
            _logger.LogError("Failed to persist user to the database: {Error}", baseUser.Error);
            return Result.Failure<BaseUser>(baseUser.Error);
        }

        _logger.LogInformation("Successfully created user {Login} with ID {UserId}", command.user.Login, baseUser.Value.PublicId);
        return Result.Success(baseUser.Value);
    }
}
