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

public class ChatService : IChatService
{
    private readonly ProfileDbContext _dbContext;
    private readonly IMapper _mapper;

    public ChatService(ProfileDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Result<BaseChat>> CreateChat(profile_Domain.Chat.Chat createChat)
    {
        try
        {
            var users = await _dbContext.Users
                .Where(u => createChat.Users.Select(id => id.PublicId).Contains(u.PublicId))
                .ToListAsync();

            // Создаем чат, назначаем найденных пользователей
            var chatEntity = _mapper.Map<ChatEntity>(createChat);
            chatEntity.Users = users; // Присваиваем найденных пользователей
       
            await _dbContext.Chats.AddAsync(chatEntity);
            await _dbContext.SaveChangesAsync();
            return Result.Success(_mapper.Map<BaseChat>(chatEntity));
        }
        catch (DbUpdateException e)
        {
            return Result.Failure<BaseChat>("An error occured while processing your request.");
        }
        catch (Exception e)
        {
            return Result.Failure<BaseChat>("An error occured while processing your request.");
        }
    }

    public async Task<Result<BaseChat>> GetChatMessage(Guid chatId)
    {
        var query = _dbContext.Chats.Where(x=>x.PublicId == chatId);
        var chat = await _mapper.ProjectTo<BaseChat>(query).FirstOrDefaultAsync();
        if (chat== null)
        {
            return Result.Failure<BaseChat>("No chat found.");
        }
        return Result.Success(chat);
    }

    
}