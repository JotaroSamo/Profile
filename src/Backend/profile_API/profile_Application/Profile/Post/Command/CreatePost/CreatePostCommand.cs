using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Post;

namespace profile_Application.Profile.Post.CreatePost;

public class CreatePostCommand : ICommand<Result<BasePost>>
{
    public profile_MapperModel.Profile.Post.CreatePost Post { get; }
    public Guid UserId { get; }

    public CreatePostCommand(profile_MapperModel.Profile.Post.CreatePost post, Guid userId)
    {
        Post = post;
        UserId = userId;
    }
}