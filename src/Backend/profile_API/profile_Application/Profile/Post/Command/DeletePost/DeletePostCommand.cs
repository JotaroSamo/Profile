using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;

namespace profile_Application.Profile.Post.Command.DeletePost;

public class DeletePostCommand : ICommand<bool>
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public DeletePostCommand(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}