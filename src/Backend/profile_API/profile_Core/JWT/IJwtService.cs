using profile_Core.Model;

namespace profile_Core.JWT;

public interface IJwtService
{
   public JwtModel GenerateJwtToken(Guid publicId, string login);
   public bool IsValidAuthToken(string token);

}