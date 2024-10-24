using uBee.Domain.Enumerations;

namespace uBee.Application.Contracts.Authentication
{
    public sealed class RegisterRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public EnUserRole UserRole { get; set; }
    }
}
