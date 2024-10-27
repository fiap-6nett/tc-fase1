using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;

namespace uBee.Domain.Entities
{
    public sealed class BeeContract : Entity<int>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Private Fields

        private List<ContractedHive> _contractedHives;

        #endregion

        #region Properties

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Price { get; private set; }
        public EnBeeContractStatus Status { get; private set; }

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

        private BeeContract()
        { }

        public BeeContract(DateTime startDate, DateTime endDate, decimal price, EnBeeContractStatus status, int idUser)
        {
            Ensure.LessThan(startDate, endDate, DomainError.BeeContract.InvalidStatusChange.Message, nameof(endDate));
            Ensure.GreaterThan(price, 0, DomainError.BeeContract.InvalidPrice.Message, nameof(price));
            Ensure.GreaterThan(idUser, 0, DomainError.User.NotFound.Message, nameof(idUser));

            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Status = status;
            IdUser = idUser;
            _contractedHives = new List<ContractedHive>();
        }

        #endregion

        #region Methods

        public void ChangeStatus(EnBeeContractStatus newStatus)
        {
            if (Status == EnBeeContractStatus.Completed || Status == EnBeeContractStatus.Cancelled)
                throw new DomainException(DomainError.BeeContract.InvalidStatusChange);

            Status = newStatus;
            LastUpdatedAt = DateTime.Now;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Ensure.GreaterThan(newPrice, 0, DomainError.BeeContract.InvalidPrice.Message, nameof(newPrice));

            Price = newPrice;
            LastUpdatedAt = DateTime.Now;
        }

        public void AddContractedHive(ContractedHive contractedHive)
        {
            Ensure.NotNull(contractedHive, DomainError.ContractedHive.InvalidHive.Message, nameof(contractedHive));
            _contractedHives.Add(contractedHive);
        }

        #endregion
    }
}
