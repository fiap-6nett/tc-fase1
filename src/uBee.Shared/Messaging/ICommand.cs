using MediatR;

namespace uBee.Shared.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
