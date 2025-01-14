namespace profile_DataAccess.Context.Entity.Profile;

public class PostEntity 
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = [];
    
    public DateTime Created { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } 
}