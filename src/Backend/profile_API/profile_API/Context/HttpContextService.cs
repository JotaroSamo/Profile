using System.Security.Principal;
using profile_Core.Contracts;

namespace profile_API.Context
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private IIdentity UserIdentity => _contextAccessor.HttpContext?.User.Identity;

        public Guid? GetCurrentUserGuid()
        {
            return Guid.TryParse(UserIdentity?.Name, out var userGuid)
                ? userGuid
                : default;
        }
    }
}