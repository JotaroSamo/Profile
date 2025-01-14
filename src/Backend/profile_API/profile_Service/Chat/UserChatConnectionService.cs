using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using profile_Core.Chat;
using profile_DataAccess;
using profile_DataAccess.Context;
using profile_DataAccess.Context.Entity.Chat;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;

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

    public async Task<bool> DeleteConnection(string connectionId)
    {
       var userChatConnectionEntity = await _profileDbContext.UserChatConnections.FirstOrDefaultAsync(x=>x.ConnectionId == connectionId);
       _profileDbContext.Remove(userChatConnectionEntity);
       await _profileDbContext.SaveChangesAsync();
       return true;
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
}