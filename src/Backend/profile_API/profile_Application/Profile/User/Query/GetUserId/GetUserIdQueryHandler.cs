using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Application.Profile.User.Query.GetUserId;
using profile_Core.Contracts;
using profile_Domain.Exception;

namespace profile_Application.Profile.User.GetUserId;

public class GetUserIdQueryHandler: IQueryHandler<GetUserIdQuery, Guid>
{
    private readonly IHttpContextService _httpContextService;

    public GetUserIdQueryHandler(IHttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }
    public async Task<Guid> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserGuid();
        if (userId == Guid.Empty)
        {
            throw new ProfileException(403,"User is not logged in");
        }
        return userId.Value;
    }
}