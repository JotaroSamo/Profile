using AutoMapper;
using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.GetAll;

using Microsoft.Extensions.Logging;

public class GetAllQueryHandler : IQueryHandler<GetAllQuery, Result<List<AllUserData>>>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetAllQueryHandler> _logger;

    public GetAllQueryHandler(IUserService userService, ILogger<GetAllQueryHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<Result<List<AllUserData>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all user data...");

        var users = await _userService.GetAll();

        if (users.IsFailure)
        {
            _logger.LogError("Failed to retrieve user data: {Error}", users.Error);
            return Result.Failure<List<AllUserData>>(users.Error);
        }

        _logger.LogInformation("Successfully retrieved {UserCount} user(s).", users.Value.Count);
        return Result.Success(users.Value);
    }
}