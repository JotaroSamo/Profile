namespace profile_MapperModel.Profile.Post;

public class BasePost
{
    public Guid PublicId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new List<string>();
    
    public DateTime Created { get; set; }
    
}