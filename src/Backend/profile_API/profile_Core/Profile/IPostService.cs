using CSharpFunctionalExtensions;
using profile_Domain.Profile;
using profile_MapperModel.Profile.Post;

namespace profile_Core.Profile;

public interface IPostService
{
  public Task<Result<BasePost>> CreatePost(Post post, Guid userId);
  public Task<Post> GetPostsById(long postId);
  public Task<Post> UpdatePost(Post post);
}