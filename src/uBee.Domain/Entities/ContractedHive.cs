using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using static uBee.Domain.Errors.DomainError;

namespace uBee.Domain.Entities
{
    public class ContractedHive : Entity<int>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        // Foreign Keys
        public int IdBeeContract { get; private set; }
        public BeeContract BeeContract { get; private set; }

        public int IdHive { get; private set; }
        public Hive Hive { get; private set; }

        #endregion

        #region Constructors

        private ContractedHive()
        { }

        public ContractedHive(int contractId, int hiveId)
        {
            Ensure.GreaterThan(contractId, 0, DomainError.BeeContract.NotFound.Message, nameof(contractId));
            Ensure.GreaterThan(hiveId, 0, DomainError.Hive.NotFound.Message, nameof(hiveId));

            IdBeeContract = contractId;
            IdHive = hiveId;
        }

        #endregion

        #region Methods

        public void UpdateHive(int newHiveId)
        {
            Ensure.GreaterThan(newHiveId, 0, DomainError.Hive.InvalidHive.Message, nameof(newHiveId));

            IdHive = newHiveId;
            LastUpdatedAt = DateTime.Now;
        }

        public void UpdateContract(int newBeeContractId)
        {
            Ensure.GreaterThan(newBeeContractId, 0, DomainError.BeeContract.NotFound.Message, nameof(newBeeContractId));

            IdBeeContract = newBeeContractId;
            LastUpdatedAt = DateTime.Now;
        }

        #endregion
    }
}
