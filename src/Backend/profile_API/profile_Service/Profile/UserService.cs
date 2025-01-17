using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using profile_Core.Profile;
using profile_DataAccess;
using profile_DataAccess.Context;
using profile_DataAccess.Context.Entity.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile;
using profile_MapperModel.Profile.Chat;
using profile_MapperModel.Profile.User;

namespace profile_Service.Profile;


public class UserService : IUserService
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly IMapper _mapper;

    public UserService(ProfileDbContext profileDbContext, IMapper mapper)
    {
        _profileDbContext = profileDbContext;
        _mapper = mapper;
    }


    public async Task<Result<BaseUser>> CreateUser(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        try
        {
            await _profileDbContext.Users.AddAsync(userEntity);
            await _profileDbContext.SaveChangesAsync();
            return Result.Success(_mapper.Map<BaseUser>(userEntity));
        }
        catch (DbUpdateException e)
        {
            return Result.Failure<BaseUser>(e.Message);
        }
        catch (Exception e)
        {
            return Result.Failure<BaseUser>("An error occured.");
        }

    }

    public async Task<User> GetUserByLogin(string login)
    {
        var user = await _profileDbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
        return _mapper.Map<User>(user);
    }

    public async Task<Result<UserPosts>> GetUserAndPostsByPublicId(Guid publicId)
    {
        var user = await _mapper.ProjectTo<UserPosts>(
                _profileDbContext.Users
                    .Include(x => x.Posts)
                    .Where(x => x.PublicId == publicId)
            )
            .FirstOrDefaultAsync();

       
        if (user == null)
        {
            return Result.Failure<UserPosts>("User not found");
        }
        if (user != null)
        {
            // Сортируем посты пользователя по времени создания
            user.Posts = user.Posts.OrderByDescending(post => post.Created).ToList();
        }
        return Result.Success(_mapper.Map<UserPosts>(user));

    }

    public async Task<Result<UserChats>> GetUserChatsByPublicId(Guid publicId)
    {
        var query = _profileDbContext.Users.Where(x=>x.PublicId==publicId).Include(x=>x.Chats).
            ThenInclude(x=>x.Users);
        var user = await _mapper.ProjectTo<UserChats>(query).FirstOrDefaultAsync();
        if (user == null)
        {
            return Result.Failure<UserChats>("User not found");
        }
        return Result.Success(user);
    }

    public async Task<Result<List<BaseUser>>> FindUsersByLogin(string login)
    {
        try
        {
            var keyword = $"%{login}%";
            var query = _profileDbContext.Users.Where(x => EF.Functions.ILike(x.Login, keyword));
            var users = await _mapper.ProjectTo<BaseUser>(query).ToListAsync();
            return Result.Success(users);
        }
        catch (Exception e)
        {
            return Result.Failure<List<BaseUser>>("An error occured.");
        }
       
        
    }

    public async Task<Result<List<AllUserData>>> GetAll()
    {
        var query = _profileDbContext.Users.Include(x=>x.Chats).
            ThenInclude(x=>x.Users).Include(x=>x.Chats).ThenInclude(x=>x.Users).Include(x=>x.Posts);
        var user = await _mapper.ProjectTo<AllUserData>(query).ToListAsync();
        if (user == null)
        {
            return Result.Failure<List<AllUserData>>("User not found");
        }
        return Result.Success(user);
    }


   
}