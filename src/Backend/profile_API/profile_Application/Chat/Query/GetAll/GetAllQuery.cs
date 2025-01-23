using CSharpFunctionalExtensions;
using profile_Application.Core.Queries.Contracts;
using profile_Domain.Profile;
using profile_MapperModel.Profile.User;

namespace profile_Application.Chat.GetAll;

public class GetAllQuery : IQuery<List<AllUserData>>
{
    public GetAllQuery()
    {
        
    }
}