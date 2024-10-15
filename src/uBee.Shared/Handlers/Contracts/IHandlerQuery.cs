using uBee.Shared.Commands;
using uBee.Shared.Queries;

namespace uBee.Shared.Handlers.Contracts
{
    public interface IHandlerQuery<TQuery> where TQuery : IQuery
    {
        #region IHandlerQuery Members
        Task<IQueryResult> Handler(TQuery query);

        #endregion
    }
}
