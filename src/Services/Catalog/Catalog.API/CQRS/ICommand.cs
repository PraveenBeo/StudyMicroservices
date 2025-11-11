using MediatR;

namespace Catalog.API.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }
    public interface ICommand<out TResponse> :IRequest<TResponse>
    {
    }
}
