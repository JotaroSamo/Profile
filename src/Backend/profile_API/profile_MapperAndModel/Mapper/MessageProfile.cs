using profile_DataAccess.Context.Entity.Chat;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;

namespace profile_MapperModel.Mapper;

public class MessageProfile: AutoMapper.Profile
{
    public MessageProfile()
    {
        CreateMap<MessageEntity, Message>().ForMember(x=>x.Chat, opt => opt.MapFrom(x=>x.ChatEntity))
            .ForMember(x=>x.User, opt => opt.MapFrom(x=>x.UserEntity)).ReverseMap();
        CreateMap<MessageEntity, BaseMessage>().ForMember(x=>x.Username, 
            opt=>opt.MapFrom(x=>x.UserEntity.Login))
            .ReverseMap();
        CreateMap<MessageEntity, CreateMessage>().ForMember(x=>x.ChatId, opt=>opt.MapFrom(x=>x.ChatId))
            .ForMember(x=>x.UserId, 
                opt=>
                    opt.MapFrom(x=>x.UserId)).ReverseMap();
        
        CreateMap<MessageEntity, CheckMessage>().ForMember(x=>x.Username, 
                opt=>
                    opt.MapFrom(x=>x.UserEntity.Login))
            .ReverseMap();
    }
    
}