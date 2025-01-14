using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace profile_Infastructure.Extensions;

public static class StringExtensions
{
    public static SymmetricSecurityKey SymmetricSecurityKey(this string accessTokenKey)
    {
        return new(Encoding.ASCII.GetBytes(accessTokenKey));
    }

    public static string FormatText(this string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var sb = new StringBuilder(text.ToLower());
            sb[0] = char.ToUpper(sb[0]);

            for (var i = 0; i < sb.Length - 2; i++)
            {
                if (sb[i] == '.' && sb[i + 1] == ' ')
                {
                    sb[i + 2] = char.ToUpper(sb[i + 2]);
                }
            }

            return sb.ToString();
        }

        return text;
    }
}