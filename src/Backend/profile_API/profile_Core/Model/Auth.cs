namespace profile_Core.Model;

public class Auth
{
    public string EncryptionKey { get; set; }
    public int AccessTokenLifetimeInHours { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}