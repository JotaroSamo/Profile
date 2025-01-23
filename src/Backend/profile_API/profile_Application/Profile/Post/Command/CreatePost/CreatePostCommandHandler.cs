using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Profile;
using profile_Domain.Exception;
using profile_MapperModel.Profile.Post;

namespace profile_Application.Profile.Post.CreatePost;

using Microsoft.Extensions.Logging;

public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, BasePost>
{
    private readonly IPostService _postService;
    private readonly ILogger<CreatePostCommandHandler> _logger;

    public CreatePostCommandHandler(IPostService postService, ILogger<CreatePostCommandHandler> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<BasePost> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreatePostRequest for user {UserId}", command.UserId);
        
        var createPost = profile_Domain.Profile.Post.Create(command.Post.Title, command.Post.Content, command.Post.Tags);
        
        if (createPost.IsFailure)
        {
            _logger.LogError("Failed to create post: {Error}", createPost.Error);
            throw new ProfileException(400, createPost.Error);
        }

        _logger.LogInformation("Creating post for user {UserId}", command.UserId);
        var post = await _postService.CreatePost(createPost.Value, command.UserId);
        
        if (post.IsFailure)
        {
            _logger.LogError("Failed to save post: {Error}", post.Error);
            throw new ProfileException(400, post.Error);
        }

        _logger.LogInformation("Post created successfully with ID: {PostId}", post.Value.PublicId);
        return post.Value;
    }
}
