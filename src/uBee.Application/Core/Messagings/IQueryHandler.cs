using MediatR;

namespace uBee.Application.Core.Messagings
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    { }
}
