using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.Post;

public class CreatePost
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new List<string>();
    
}