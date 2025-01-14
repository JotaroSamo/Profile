using profile_DataAccess.Entity.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.Post;

namespace profile_MapperModel.Mapper;

public class PostProfile : AutoMapper.Profile
{
    public PostProfile()
    {
        CreateMap<PostEntity, Post>().ForMember(x=>x.User, ctor => ctor.MapFrom(x=>x.User))
            .ForMember(x=>x.UserId, ctor=>ctor.MapFrom(x=>x.UserId)).ReverseMap();
        CreateMap<PostEntity, BasePost>().ReverseMap();
        CreateMap<PostEntity, CreatePost>().ReverseMap();
    }
}