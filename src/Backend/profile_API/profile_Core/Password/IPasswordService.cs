using profile_Core.Model;

namespace profile_Core.Password;

public interface IPasswordService
{
    byte[] GenerateSalt();
    HashModel HashPassword(string password, byte[] salt);
    bool VerifyPassword(HashModel model,string password);
}