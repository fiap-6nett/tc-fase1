using uBee.Shared.Commands;

namespace uBee.Shared.Handlers.Contracts
{
    public interface IHandlerCommand<TCommand> where TCommand : ICommand
    {
        #region IHandlerCommand Members
        Task<ICommandResult> Handler(TCommand command);

        #endregion
    }
}
