using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;

namespace uBee.Domain.Entities
{
    public class Hive : AggregateRoot<int>, IAuditableEntity, ISoftDeletableEntity
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
        public int IdUser { get; private set; }
        public User User { get; private set; }

        // Compositions
        public IReadOnlyCollection<ContractedHive> ContractedHives => _contractedHives.AsReadOnly();

        #endregion

        #region Constructors

        private Hive()
        { }

        public Hive(string description, EnHiveStatus status, int idUser)
        {
            Ensure.NotEmpty(description, DomainError.Hive.InvalidHive.Message, nameof(description));
            Ensure.GreaterThan(idUser, 0, DomainError.User.NotFound.Message, nameof(idUser));

            Description = description;
            Status = status;
            IdUser = idUser;
            _contractedHives = new List<ContractedHive>();
        }

        #endregion

        #region Methods

        public void AddContractedHive(ContractedHive contractedHive)
        {
            Ensure.NotNull(contractedHive, DomainError.ContractedHive.InvalidHive.Message, nameof(contractedHive));
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

        #endregion
    }
}
