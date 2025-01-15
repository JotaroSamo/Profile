using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;

namespace profile_Application.Chat.DeleteConnection;

public class DeleteConnectionCommand : ICommand<Result<bool>>
{
    public string ConnectionId { get; }


    public DeleteConnectionCommand(string connectionId)
    {
        ConnectionId = connectionId;
     
    }
}