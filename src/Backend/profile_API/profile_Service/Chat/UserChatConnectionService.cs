using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using profile_Core.Chat;
using profile_DataAccess;
using profile_DataAccess.Context;
using profile_DataAccess.Context.Entity.Chat;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;
using profile_MapperModel.Profile.User;

namespace profile_Service.Chat;

public class UserChatConnectionService : IUserChatConnectionService
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly IMapper _mapper;

    public UserChatConnectionService(ProfileDbContext profileDbContext, IMapper 
        mapper)
    {
        _profileDbContext = profileDbContext;
        _mapper = mapper;
    }
    public async Task<Result<BaseUserChatConnection>> CreateConnection(UserChatConnection userChatConnection)
    {
        var user = await _profileDbContext.Users.FirstOrDefaultAsync(x=>x.PublicId == userChatConnection.UserId);
        var userChatConnectionEntity = _mapper.Map<UserChatConnectionEntity>(userChatConnection);
        userChatConnectionEntity.User = user;
        await _profileDbContext.UserChatConnections.AddAsync(userChatConnectionEntity);
        await _profileDbContext.SaveChangesAsync();
        return Result.Success<BaseUserChatConnection>(_mapper.Map<BaseUserChatConnection>(userChatConnectionEntity));
        
    }

    public async Task<Result<bool>> DeleteConnection(string conectionId)
    {
       var userChatConnectionEntitys = await _profileDbContext.UserChatConnections.Where(c=>c.ConnectionId == conectionId).ToListAsync();
       if (userChatConnectionEntitys == null)
           return Result.Failure<bool>("UserChatConnection not found");
       var connectionIds = userChatConnectionEntitys.Select(x=>x.ConnectionId).ToList();
       try
       {
           _profileDbContext.RemoveRange(userChatConnectionEntitys);
           await _profileDbContext.SaveChangesAsync();
       }
       catch (Exception e)
       {
           return Result.Failure<bool>("UserChatConnection not found");
       }
     
       return Result.Success(true);
    }

    public async Task<Result<List<BaseUserChatConnection>>> GetConnections(Guid chatId)
    {
        var query = _profileDbContext.UserChatConnections.Where(x=>x.ChatId == chatId);
        var baseUserChatConnections = await _mapper.ProjectTo<BaseUserChatConnection>(query).ToListAsync();
        if (baseUserChatConnections == null)
        {
            return Result.Failure<List<BaseUserChatConnection>>("No connections found");
        }
        return Result.Success<List<BaseUserChatConnection>>(baseUserChatConnections);
    }
    public async Task<Result<List<BaseUser>>> GetChatUsers(Guid chatId)
    {
        var query = _profileDbContext.UserChatConnections.Where(x=>x.ChatId == chatId).Select(x=>x.User).AsQueryable();
        var users = await _mapper.ProjectTo<BaseUser>(query).ToListAsync();
        if (users == null)
        {
            return Result.Failure<List<BaseUser>>("No user found.");
        }
        return Result.Success(users);
    }
}