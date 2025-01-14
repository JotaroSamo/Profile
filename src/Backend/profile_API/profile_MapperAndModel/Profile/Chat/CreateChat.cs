using System.ComponentModel.DataAnnotations;

namespace profile_MapperModel.Profile.Chat;

public class CreateChat
{ 
    [Required]
    public string Title { get; set; }
    
    public List<Guid> UsersIds { get; set; }
}