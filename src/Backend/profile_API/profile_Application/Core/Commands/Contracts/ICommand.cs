
using MediatR;

namespace profile_Application.Core.Commands.Contracts
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}