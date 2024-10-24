using MediatR;

namespace uBee.Shared.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
