using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Messagings;
using uBee.Domain.Enumerations;
using uBee.Domain.ValueObjects;

namespace uBee.Application.Authentication
{
    public record RegisterCommand(string Name, string Surname, CPF Cpf, Email Email, string Password, Phone Phone, EnUserRole UserRole) : ICommand<TokenResponse>;
}
