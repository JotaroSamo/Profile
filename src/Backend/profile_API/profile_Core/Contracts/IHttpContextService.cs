namespace profile_Core.Contracts;

    public interface IHttpContextService
    {
        Guid? GetCurrentUserGuid();
    }