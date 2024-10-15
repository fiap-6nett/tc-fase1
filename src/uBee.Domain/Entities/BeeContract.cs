using Flunt.Notifications;
using Flunt.Validations;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;

namespace uBee.Domain.Entities
{
    public class BeeContract : EntityBase, IAuditableEntity, ISoftDeletableEntity
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
        public Guid IdUser { get; private set; }
        public User User { get; private set; }

        // Relationship
        public IReadOnlyCollection<ContractedHive> ContractedHives => _contractedHives.AsReadOnly();

        #endregion

        #region Constructors
        private BeeContract()
        {
            _contractedHives = new List<ContractedHive>();
        }

        public BeeContract(DateTime startDate, DateTime endDate, decimal price, EnBeeContractStatus status, Guid idUser)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsLowerThan(startDate, endDate, nameof(endDate), "The end date must be later than the start date.")
                    .IsGreaterThan(price, 0, nameof(price), "The price must be greater than zero.")
                    .IsFalse(idUser == Guid.Empty, nameof(idUser), "The user id is required.")
            );

            if (IsValid)
            {
                StartDate = startDate;
                EndDate = endDate;
                Price = price;
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

        public void ChangeStatus(EnBeeContractStatus newStatus)
        {
            if (Status == EnBeeContractStatus.Completed || Status == EnBeeContractStatus.Cancelled)
                throw new DomainException(DomainError.BeeContract.InvalidStatusChange);

            Status = newStatus;
            LastUpdatedAt = DateTime.Now;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomainException(DomainError.BeeContract.InvalidPrice);

            Price = newPrice;
            LastUpdatedAt = DateTime.Now;
        }

        public void AddContractedHive(ContractedHive contractedHive)
        {
            if (contractedHive == null)
                throw new DomainException(DomainError.ContractedHive.InvalidHive);

            _contractedHives.Add(contractedHive);
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
