using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.ValueObjects;

namespace uBee.Domain.Entities
{
    public class User : AggregateRoot<int>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Private Fields

        private string _passwordHash;
        private List<Hive> _hives;
        private List<BeeContract> _beeContracts;

        #endregion

        #region Properties

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public CPF CPF { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public byte UserRole { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        // Foreign Keys
        public byte LocationId { get; private set; }
        public Location Location { get; private set; }

        // Compositions
        public IReadOnlyCollection<Hive> Hives => _hives.AsReadOnly();
        public IReadOnlyCollection<BeeContract> BeeContracts => _beeContracts.AsReadOnly();

        #endregion

        #region Constructors

        private User()
        { }

        public User(string name, string surname, CPF cpf, Email email, Phone phone, string passwordHash, byte userRole, byte locationId)
        {
            Ensure.NotEmpty(name, DomainError.User.NameIsRequired.Message, nameof(name));
            Ensure.NotEmpty(surname, DomainError.User.SurnameIsRequired.Message, nameof(surname));
            Ensure.NotEmpty(cpf, DomainError.Cpf.InvalidFormat.Message, nameof(cpf));
            Ensure.NotEmpty(email, DomainError.Email.NullOrEmpty.Message, nameof(email));
            Ensure.NotEmpty(phone, DomainError.General.UnProcessableRequest.Message, nameof(phone));
            Ensure.NotEmpty(passwordHash, DomainError.Password.NullOrEmpty.Message, nameof(passwordHash));
            Ensure.GreaterThan(locationId, 0, DomainError.Location.InvalidAreaCode.Message, nameof(locationId));

            if (!Enum.IsDefined(typeof(EnUserRole), userRole))
                throw new ArgumentException(DomainError.UserRole.NotFound.Message, nameof(userRole));

            Name = name;
            Surname = surname;
            CPF = cpf;
            Email = email;
            Phone = phone;
            _passwordHash = passwordHash;
            UserRole = userRole;
            LocationId = locationId;
            CreatedAt = DateTime.Now;
            _hives = new List<Hive>();
            _beeContracts = new List<BeeContract>();
        }

        #endregion

        #region Methods

        public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
        {
            return !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (newPasswordHash == _passwordHash)
                throw new DomainException(DomainError.User.CannotChangePassword);

            _passwordHash = newPasswordHash;
            LastUpdatedAt = DateTime.Now;
        }

        public void ChangeName(string newName, string newSurname)
        {
            Ensure.NotEmpty(newName, DomainError.User.NameIsRequired.Message, nameof(newName));
            Ensure.NotEmpty(newSurname, DomainError.User.SurnameIsRequired.Message, nameof(newSurname));

            Name = newName;
            Surname = newSurname;
            LastUpdatedAt = DateTime.Now;
        }

        public void SoftDelete()
        {
            if (IsDeleted)
                throw new DomainException(DomainError.User.AlreadyDeleted);

            IsDeleted = true;
            LastUpdatedAt = DateTime.Now;
        }

        #endregion
    }
}
