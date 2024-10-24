using uBee.Domain.Enumerations;

namespace uBee.Application.Contracts.Authentication
{
    /// <summary>
    /// Represents a request to register a new user in the system.
    /// </summary>
    public sealed class RegisterRequest
    {
        /// <summary>
        /// Gets or sets the first name of the user being registered.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user being registered.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the CPF (Cadastro de Pessoas Físicas) of the user being registered.
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Gets or sets the email of the user being registered.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user being registered.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user being registered.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the role of the user being registered.
        /// </summary>
        public EnUserRole UserRole { get; set; }
    }
}
