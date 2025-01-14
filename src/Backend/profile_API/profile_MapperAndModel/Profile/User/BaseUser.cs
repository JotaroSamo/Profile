namespace profile_MapperModel.Profile.User;

public class BaseUser
{
    public Guid PublicId { get; set; }

    public string Login { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string? AvatarUrl { get; set; }
}