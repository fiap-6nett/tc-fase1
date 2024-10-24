using uBee.Application.Contracts.Authentication;
using uBee.Domain.Enumerations;
using uBee.Domain.ValueObjects;
using uBee.Shared.Messaging;

namespace uBee.Application.Authentication
{
    public record RegisterCommand(string Name, string Surname, CPF Cpf, Email Email, string Password, Phone Phone, EnUserRole UserRole) : ICommand<TokenResponse>;
}
