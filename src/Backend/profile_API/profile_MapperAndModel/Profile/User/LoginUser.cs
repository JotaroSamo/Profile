using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.User;

using System.ComponentModel.DataAnnotations;

public class LoginUser
{ 
    [Required(ErrorMessage = "Login is required.")]
    [StringLength(100, ErrorMessage = "Login must be between 3 and 100 characters.", MinimumLength = 3)]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
