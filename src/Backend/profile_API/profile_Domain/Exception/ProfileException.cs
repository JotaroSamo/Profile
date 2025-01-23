using profile_Domain.Exception.Base;

namespace profile_Domain.Exception;

public class ProfileException : ProfileExceptionBase
{
    public ProfileException(int statusCode, string errorMessage)
        : base(statusCode, errorMessage)
    {
    }
}
