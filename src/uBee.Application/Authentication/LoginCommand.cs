using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Messagings;

namespace uBee.Application.Authentication
{
    public record LoginCommand(string Email, string Password) : ICommand<TokenResponse>;
}
