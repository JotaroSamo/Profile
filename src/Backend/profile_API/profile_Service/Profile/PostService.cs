using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using profile_Core.Profile;
using profile_DataAccess;
using profile_DataAccess.Context;
using profile_DataAccess.Context.Entity.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.Post;

namespace profile_Service.Profile;

public class PostService : IPostService
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly IMapper _mapper;

    public PostService(ProfileDbContext profileDbContext, IMapper mapper)
    {
        _profileDbContext = profileDbContext;
        _mapper = mapper;
    }
    public async Task<Result<BasePost>> CreatePost(Post post, Guid userId)
    {
        try
        {
            
            var postEntity = _mapper.Map<PostEntity>(post);
            postEntity.Created = DateTime.UtcNow;
            postEntity.UserId = userId;
            await _profileDbContext.AddAsync(postEntity);
            await _profileDbContext.SaveChangesAsync();
            return  Result.Success(_mapper.Map<BasePost>(postEntity));
        }
        catch (Exception e)
        {
           return Result.Failure<BasePost>("Failed to create post");
        }
        
    }

    public async Task<Post> GetPostsById(long postId)
    {
        return await _mapper.ProjectTo<Post>(_profileDbContext.Posts.Where(x=>x.Id== postId)).FirstOrDefaultAsync();
    }

    public async Task<Post> UpdatePost(Post post)
    {
        var postEntity = await _profileDbContext.Posts.FirstOrDefaultAsync(x => x.Id == post.Id);
        postEntity.Title = post.Title;
        postEntity.Content = post.Content;
        postEntity.Tags = post.Tags;
        await _profileDbContext.SaveChangesAsync();
        return _mapper.Map<Post>(postEntity);
    }
}