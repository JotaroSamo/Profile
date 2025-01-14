


using CSharpFunctionalExtensions;
using MediatR;
using profile_MapperModel.Profile.User;

namespace profile_Application.Core.Commands.Contracts
{
    public interface ICommandHandler<in TCommand> :
        IRequestHandler<TCommand> where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
      
    }
}