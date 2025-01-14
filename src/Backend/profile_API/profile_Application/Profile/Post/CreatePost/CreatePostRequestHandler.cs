using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Profile;
using profile_MapperModel.Profile.Post;

namespace profile_Application.Profile.Post.CreatePost;

using Microsoft.Extensions.Logging;

public class CreatePostRequestHandler : ICommandHandler<CreatePostRequest, Result<BasePost>>
{
    private readonly IPostService _postService;
    private readonly ILogger<CreatePostRequestHandler> _logger;

    public CreatePostRequestHandler(IPostService postService, ILogger<CreatePostRequestHandler> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<Result<BasePost>> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreatePostRequest for user {UserId}", request.UserId);
        
        var createPost = profile_Domain.Profile.Post.Create(request.Post.Title, request.Post.Content, request.Post.Tags);
        
        if (createPost.IsFailure)
        {
            _logger.LogError("Failed to create post: {Error}", createPost.Error);
            return Result.Failure<BasePost>(createPost.Error);
        }

        _logger.LogInformation("Creating post for user {UserId}", request.UserId);
        var post = await _postService.CreatePost(createPost.Value, request.UserId);
        
        if (post.IsFailure)
        {
            _logger.LogError("Failed to save post: {Error}", post.Error);
            return Result.Failure<BasePost>(post.Error);
        }

        _logger.LogInformation("Post created successfully with ID: {PostId}", post.Value.PublicId);
        return post;
    }
}
