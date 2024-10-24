using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility;
using uBee.Domain.Errors;

namespace uBee.Domain.Entities
{
    public class Location : Entity<byte>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties

        public string Name { get; private set; }
        public int Number { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        // Compositions
        public IReadOnlyCollection<User> Users { get; private set; }

        #endregion

        #region Constructors

        private Location()
        { }

        public Location(string name, int number)
        {
            Ensure.NotEmpty(name, DomainError.General.UnProcessableRequest.Message, nameof(name));
            Ensure.GreaterThan(number, 0, DomainError.General.InvalidDDD.Message, nameof(number));

            Name = name;
            Number = number;
            Users = new List<User>();
        }

        #endregion
    }
}
