using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.User;

public class CreateUser
{
    [Required]
    public string Login { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl { get; set; }
    [Required]
    public string Password { get; set; } = string.Empty;
}