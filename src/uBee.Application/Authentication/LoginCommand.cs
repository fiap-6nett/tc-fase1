using uBee.Application.Contracts.Authentication;
using uBee.Shared.Messaging;

namespace uBee.Application.Authentication
{
    public record LoginCommand(string Email, string Password) : ICommand<TokenResponse>;
}
