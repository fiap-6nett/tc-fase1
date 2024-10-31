using MediatR;

namespace uBee.Application.Core.Messagings
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { }
}
