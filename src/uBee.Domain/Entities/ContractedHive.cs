using uBee.Domain.Core.Primitives;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;

namespace uBee.Domain.Entities
{
    public class ContractedHive : EntityBase, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties

        // Foreign Keys
        public Guid IdBeeContract { get; private set; }
        public BeeContract BeeContract { get; private set; }

        public Guid IdHive { get; private set; }
        public Hive Hive { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        #endregion

        #region Constructors
        private ContractedHive() { }

        public ContractedHive(Guid contractId, Guid hiveId)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsFalse(contractId == Guid.Empty, nameof(contractId), "The contract id is required.")
                    .IsFalse(hiveId == Guid.Empty, nameof(hiveId), "The hive id is required.")
            );

            if (IsValid)
            {
                IdBeeContract = contractId;
                IdHive = hiveId;
                CreatedAt = DateTime.Now;
                LastUpdatedAt = null;
                IsDeleted = false;
            }
        }
        #endregion

        #region Methods
        public void UpdateHive(Guid newHiveId)
        {
            if (newHiveId == Guid.Empty)
                throw new DomainException(DomainError.ContractedHive.InvalidHive);

            IdHive = newHiveId;
            LastUpdatedAt = DateTime.Now;
        }

        public void UpdateContract(Guid newBeeContractId)
        {
            if (newBeeContractId == Guid.Empty)
                throw new DomainException(DomainError.ContractedHive.InvalidContract);

            IdBeeContract = newBeeContractId;
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
