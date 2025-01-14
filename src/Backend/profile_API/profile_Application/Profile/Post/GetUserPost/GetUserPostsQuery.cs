using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_MapperModel.Profile.User;

namespace profile_Application.Profile.Post.GetUserPost;

public class GetUserPostsQuery : IQuery<Result<UserPosts>>
{
    public Guid UserId { get; }

    public GetUserPostsQuery(Guid userId)
    {
        UserId = userId;
    }
}