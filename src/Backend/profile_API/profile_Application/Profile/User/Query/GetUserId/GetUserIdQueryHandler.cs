using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Core.Contracts;

namespace profile_Application.Profile.User.GetUserId;

public class GetUserIdQueryHandler: IQueryHandler<GetUserIdQuery, Result<Guid>>
{
    private readonly IHttpContextService _httpContextService;

    public GetUserIdQueryHandler(IHttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }
    public async Task<Result<Guid>> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserGuid();
        if (userId == Guid.Empty)
        {
            return Result.Failure<Guid>("User is not logged in");
        }
        return Result.Success(userId.Value);
    }
}