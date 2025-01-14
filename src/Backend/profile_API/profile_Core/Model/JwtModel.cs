using System.Text.Json.Serialization;

namespace profile_Core.Model;

public class JwtModel
{
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
}