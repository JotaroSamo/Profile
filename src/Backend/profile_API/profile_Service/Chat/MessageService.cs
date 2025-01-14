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

public class MessageService : IMessageService
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly IMapper _mapper;

    public MessageService(ProfileDbContext profileDbContext, IMapper 
        mapper)
    {
        _profileDbContext = profileDbContext;
        _mapper = mapper;
    }
    public async Task<Result<BaseMessage>> CreateMessage(Message message)
    {
        try
        {
            var chat = await _profileDbContext.Chats.FirstOrDefaultAsync(x=>x.PublicId == message.ChatId);
            var user = await _profileDbContext.Users.FirstOrDefaultAsync(x=>x.PublicId == message.UserId);
            if (chat == null || user == null)
            {
                // Можно вернуть ошибку, если чат или пользователь не найдены
                return Result.Failure<BaseMessage>("Chat or User does not exist.");
            }

            var messageEntity = _mapper.Map<MessageEntity>(message);
            messageEntity.ChatEntity = chat;
            messageEntity.UserEntity = user;
            await _profileDbContext.Messages.AddAsync(messageEntity);
            await _profileDbContext.SaveChangesAsync();
            return Result.Success(_mapper.Map<BaseMessage>(messageEntity));
        }
        catch (Exception e)
        {
            return Result.Failure<BaseMessage>("Failed to create message");
        }
       
    }
}