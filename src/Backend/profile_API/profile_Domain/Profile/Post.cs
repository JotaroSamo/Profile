using CSharpFunctionalExtensions;

namespace profile_Domain.Profile;

public class Post
{
    public long Id { get; set; }
    
    public Guid PublicId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new List<string>();
    
    public DateTime Created { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Post()
    {
    }

    private Post(string title, string content, List<string> tags)
    {
        PublicId = Guid.NewGuid();
        Title = title;
        Content = content;
        Tags = tags;
        Created = DateTime.UtcNow; // Устанавливаем время создания на момент создания поста
    }

    public static Result<Post> Create(string title, string content, List<string> tags)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Post>("Invalid title");
        }
        if (string.IsNullOrWhiteSpace(content))
        {
            return Result.Failure<Post>("Invalid content");
        }


        var post = new Post(title, content, tags);
        return Result.Success(post);
    }
}
