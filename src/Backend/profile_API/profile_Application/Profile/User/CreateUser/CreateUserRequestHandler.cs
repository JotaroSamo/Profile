using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Password;
using profile_Core.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.CreateUser;

using Microsoft.Extensions.Logging;

public class CreateUserRequestHandler : ICommandHandler<CreateUserRequest, Result<BaseUser>>
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<CreateUserRequestHandler> _logger;

    public CreateUserRequestHandler(IUserService userService, IPasswordService passwordService, ILogger<CreateUserRequestHandler> logger)
    {
        _userService = userService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<Result<BaseUser>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateUserRequest for user {Login}", request.user.Login);
        
        // Генерация соли и хеширование пароля
        var salt = _passwordService.GenerateSalt();
        var hasModel = _passwordService.HashPassword(request.user.Password, salt);
        
        var user = profile_Domain.Profile.User.Create(
            request.user.Login,
            request.user.FirstName,
            request.user.AvatarUrl,
            request.user.LastName,
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

        _logger.LogInformation("Successfully created user {Login} with ID {UserId}", request.user.Login, baseUser.Value.PublicId);
        return Result.Success(baseUser.Value);
    }
}
