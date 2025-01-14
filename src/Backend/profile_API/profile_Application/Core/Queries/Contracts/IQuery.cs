using MediatR;

namespace profile_Application.Core.Queries.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}