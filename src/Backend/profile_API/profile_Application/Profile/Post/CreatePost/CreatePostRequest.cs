using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;
using profile_MapperModel.Profile.Post;

namespace profile_Application.Profile.Post.CreatePost;

public class CreatePostRequest : ICommand<Result<BasePost>>
{
    public profile_MapperModel.Profile.Post.CreatePost Post { get; }
    public Guid UserId { get; }

    public CreatePostRequest(profile_MapperModel.Profile.Post.CreatePost post, Guid userId)
    {
        Post = post;
        UserId = userId;
    }
}