using profile_DataAccess.Context.Entity.Chat;
using profile_Domain.Chat;
using profile_MapperModel.Profile.Chat;

namespace profile_MapperModel.Mapper;

public class ChatProfile : AutoMapper.Profile
{
    public ChatProfile()
    {
        CreateMap<ChatEntity, Chat>().ForMember(x=>x.Users, y=>y.MapFrom(z=>z.Users)).ReverseMap();
        CreateMap<ChatEntity, BaseChat>().ForMember(x=>x.Messages, opt=>opt.MapFrom(x=>x.Messages))
            .ForMember(x=>x.Users, opt=>opt.MapFrom(x=>x.Users)).ReverseMap();
        CreateMap<ChatEntity, CreateChat>()
            .ForMember(dest => dest.UsersIds, opt => opt.MapFrom(src => src.Users.Select(u => u.PublicId)))
            .ReverseMap();
        CreateMap<ChatEntity, ChatEmty>().ReverseMap();

    }
}