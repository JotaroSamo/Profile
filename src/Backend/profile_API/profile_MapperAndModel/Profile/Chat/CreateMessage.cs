using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.Chat;

public class CreateMessage
{
    [Required]
    public string Content { get; set; }
    [Required]
    public Guid ChatId { get; set; }
    [Required]
    public Guid UserId { get; set; }
}