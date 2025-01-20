using CSharpFunctionalExtensions;
using profile_Domain.Profile;
using profile_MapperModel.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Core.Profile;

public interface IUserService
{
  
    public Task<Result<BaseUser>> CreateUser(User user);
    public Task<User> GetUserByLogin(string login);
    public Task<Result<UserPosts>> GetUserAndPostsByPublicId(Guid publicId);
    
    public Task<Result<UserChats>> GetUserChatsByPublicId(Guid publicId);
    
      public  Task<Result<List<BaseUser>>> FindUsersByQuery(string query);
    public Task<Result<List<AllUserData>>> GetAll();
}