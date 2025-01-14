using AutoMapper;
using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.Post.GetUserPost;

using Microsoft.Extensions.Logging;

public class GetUserPostsQueryHandler : IQueryHandler<GetUserPostsQuery, Result<UserPosts>>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetUserPostsQueryHandler> _logger;

    public GetUserPostsQueryHandler(IUserService userService, ILogger<GetUserPostsQueryHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<Result<UserPosts>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetUserPostsQuery for user ID {UserId}", request.UserId);

        var user = await _userService.GetUserAndPostsByPublicId(request.UserId);
        if (user.IsFailure)
        {
            _logger.LogError("Failed to retrieve posts for user ID {UserId}: {Error}", request.UserId, user.Error);
            return Result.Failure<UserPosts>(user.Error);
        }

        _logger.LogInformation("Successfully retrieved posts for user ID {UserId}", request.UserId);
        return Result.Success(user.Value);
    }
}
