using Flunt.Notifications;
using Flunt.Validations;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace uBee.Domain.Entities
{
    public class User : EntityBase, IAuditableEntity, ISoftDeletableEntity
    {
        #region Private Fields

        private string _passwordHash;
        private List<Hive> _hives;
        private List<BeeContract> _beeContracts;

        #endregion

        #region Properties
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public EnUserRole UserRole { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        // Foreign Keys
        public Guid IdLocation { get; private set; }
        public Location Location { get; private set; }

        // Relationship
        public IReadOnlyCollection<Hive> Hives => _hives.AsReadOnly();
        public IReadOnlyCollection<BeeContract> BeeContracts => _beeContracts.AsReadOnly();

        #endregion

        #region Constructors
        private User() { }

        public User(string name, string surname, string email, string phone, string passwordHash, EnUserRole userRole, Guid idLocation)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(name, nameof(name), "The name is required.")
                    .IsNotEmpty(surname, nameof(surname), "The surname is required.")
                    .IsEmail(email, nameof(email), "Please provide a valid email address.")
                    .IsNotEmpty(phone, nameof(phone), "The phone number is required.")
                    .IsNotEmpty(passwordHash, nameof(passwordHash), "The password is required.")
                    .IsTrue(Enum.IsDefined(typeof(EnUserRole), userRole), nameof(userRole), "The user role is invalid.")
                    .IsFalse(idLocation == Guid.Empty, nameof(idLocation), "The location id is required.")
            );

            if (IsValid)
            {
                Name = name;
                Surname = surname;
                Email = email;
                Phone = phone;
                _passwordHash = passwordHash;
                UserRole = userRole;
                IdLocation = idLocation;
                CreatedAt = DateTime.Now;
                LastUpdatedAt = null;
                IsDeleted = false;
                _hives = new List<Hive>();
                _beeContracts = new List<BeeContract>();
            }
        }

        #endregion

        #region Methods
        public bool VerifyPassword(string password)
        {
            return _passwordHash == password;
        }

        public void ChangePassword(string passwordHash)
        {
            if (passwordHash == _passwordHash)
                throw new DomainException(DomainError.User.CannotChangePassword);

            _passwordHash = passwordHash;
            LastUpdatedAt = DateTime.Now;
        }

        public void ChangeName(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(DomainError.User.NameIsRequired);

            if (string.IsNullOrWhiteSpace(surname))
                throw new DomainException(DomainError.User.SurnameIsRequired);

            Name = name;
            Surname = surname;
            LastUpdatedAt = DateTime.Now;
        }

        public void AddHive(Hive hive)
        {
            if (hive == null)
                throw new DomainException(DomainError.Hive.InvalidHive);

            _hives.Add(hive);
        }

        public void AddBeeContract(BeeContract beeContract)
        {
            if (beeContract == null)
                throw new DomainException(DomainError.BeeContract.InvalidUser);

            _beeContracts.Add(beeContract);
        }

        public void MarkAsDeleted()
        {
            if (IsDeleted)
                throw new DomainException(DomainError.General.AlreadyDeleted);

            IsDeleted = true;
            LastUpdatedAt = DateTime.Now;
        }

        #endregion
    }
}
