using profile_DataAccess.Entity.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.User;

namespace profile_MapperModel.Mapper;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, AllUserData>().ForMember(x=>x.Posts, ctor => ctor.MapFrom(x=>x.Posts))
            .ForMember(x => x.Chats, opt => opt.MapFrom(x => x.Chats)).ReverseMap();
        CreateMap<UserEntity, User>().ForMember(x=>x.Posts, ctor => ctor.MapFrom(x=>x.Posts)).ReverseMap();
        CreateMap<UserEntity, BaseUser>().ReverseMap();
        CreateMap<UserEntity, UserPosts>().ForMember(x=>x.Posts, ctor => ctor.MapFrom(x=>x.Posts)).ReverseMap();
        CreateMap<UserEntity, CreateUser>().ReverseMap();
        CreateMap<UserEntity, UserChats>().ForMember(x => x.Chats, opt => opt.MapFrom(x => x.Chats)).ReverseMap();

    }
}