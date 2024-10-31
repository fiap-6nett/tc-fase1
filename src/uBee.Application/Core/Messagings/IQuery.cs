using MediatR;

namespace uBee.Application.Core.Messagings
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
