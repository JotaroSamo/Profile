namespace profile_Domain.Exception.Base;

public abstract class ProfileExceptionBase : System.Exception
{
    public int StatusCode { get; private set; }
    public string ErrorMessage { get; private set; }

    protected ProfileExceptionBase(int statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public override string ToString()
    {
        return $"Status Code: {StatusCode}, Error Message: {ErrorMessage}";
    }
}
