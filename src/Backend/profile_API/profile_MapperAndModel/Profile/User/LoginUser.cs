using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.User;

public class LoginUser
{ 
    [Required]
    public string Login { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}