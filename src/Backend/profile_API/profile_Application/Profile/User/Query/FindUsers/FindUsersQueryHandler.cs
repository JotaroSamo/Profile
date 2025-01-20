using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.User.Query.FindUsers;

public class FindUsersQueryHandler : IQueryHandler<FindUsersQuery, Result<List<BaseUser>>>
{
    private readonly IUserService _userService;
    private readonly ILogger<FindUsersQueryHandler> _logger;

    public FindUsersQueryHandler(IUserService userService, ILogger<FindUsersQueryHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    public async Task<Result<List<BaseUser>>> Handle(FindUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userService.FindUsersByQuery(request.Query);
        if (users.IsFailure)
        {
            return Result.Failure<List<BaseUser>>(users.Error);
        }
        return Result.Success(users.Value);
    }
}