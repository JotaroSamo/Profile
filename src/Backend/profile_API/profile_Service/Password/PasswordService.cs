using profile_Core.Model;
using profile_Core.Password;

namespace profile_Service.Password;

public class PasswordService : IPasswordService
{
    public  byte[] GenerateSalt()
    {
        int size = 16;
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            var salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }
    }
    public HashModel HashPassword(string password, byte[] salt)
    {
    
        using (var rfc2898DeriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 10000))
        {
            return  new HashModel()
            {
                HashPassword = rfc2898DeriveBytes.GetBytes(32),
                Salt = salt,
            };
        }
    }
    public bool VerifyPassword(HashModel model,string password)
    {
        var hashToVerify = HashPassword(password, model.Salt);
        return model.HashPassword.ToString() == hashToVerify.HashPassword.ToString();
    }



}