using CSharpFunctionalExtensions;
using profile_Application.Core.Commands.Contracts;

namespace profile_Application.Chat.Command.DeleteConnection;

public class DeleteConnectionCommand : ICommand<bool>
{
    public string ConnectionId { get; }


    public DeleteConnectionCommand(string connectionId)
    {
        ConnectionId = connectionId;
     
    }
}