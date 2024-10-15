using Flunt.Notifications;
using Flunt.Validations;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using static uBee.Domain.Errors.DomainError;

namespace uBee.Domain.Entities
{
    public class Hive : EntityBase, IAuditableEntity, ISoftDeletableEntity
    {
        #region Private Fields
        private List<ContractedHive> _contractedHives;

        #endregion

        #region Properties
        public string Description { get; private set; }
        public EnHiveStatus Status { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        // Foreign Keys
        public Guid IdUser { get; private set; }
        public User User { get; private set; }

        // Relationship
        public IReadOnlyCollection<ContractedHive> ContractedHives => _contractedHives.AsReadOnly();

        #endregion

        #region Constructors
        private Hive()
        {
            _contractedHives = new List<ContractedHive>();
        }

        public Hive(string description, EnHiveStatus status, Guid idUser)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(description, nameof(description), "The description is required.")
                    .IsFalse(idUser == Guid.Empty, nameof(idUser), "The user id is required.")
            );

            if (IsValid)
            {
                Description = description;
                Status = status;
                IdUser = idUser;
                CreatedAt = DateTime.Now;
                LastUpdatedAt = null;
                IsDeleted = false;
                _contractedHives = new List<ContractedHive>();
            }
        }

        #endregion

        #region Methods

        public void AddContractedHive(ContractedHive contractedHive)
        {
            if (contractedHive == null)
                throw new DomainException(DomainError.ContractedHive.InvalidHive);

            _contractedHives.Add(contractedHive);
        }

        public void MarkAsInUse()
        {
            if (Status != EnHiveStatus.Available)
                throw new DomainException(DomainError.Hive.CannotMarkInUse);

            Status = EnHiveStatus.InUse;
            LastUpdatedAt = DateTime.Now;
        }

        public void MarkAsAvailable()
        {
            if (Status == EnHiveStatus.Decommissioned)
                throw new DomainException(DomainError.Hive.CannotMarkAvailable);

            Status = EnHiveStatus.Available;
            LastUpdatedAt = DateTime.Now;
        }

        public void MarkAsUnderMaintenance()
        {
            if (Status == EnHiveStatus.InUse)
                throw new DomainException(DomainError.Hive.CannotMarkUnderMaintenance);

            Status = EnHiveStatus.UnderMaintenance;
            LastUpdatedAt = DateTime.Now;
        }

        public void Decommission()
        {
            if (Status == EnHiveStatus.InUse)
                throw new DomainException(DomainError.Hive.CannotDecommission);

            Status = EnHiveStatus.Decommissioned;
            LastUpdatedAt = DateTime.Now;
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
