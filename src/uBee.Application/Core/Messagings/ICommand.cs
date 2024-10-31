using MediatR;

namespace uBee.Application.Core.Messagings
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
