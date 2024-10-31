using uBee.Domain.Entities;
using uBee.Domain.ValueObjects;

namespace uBee.Application.UnitTests.TestEntities
{
    internal class UserTest : User
    {
        public UserTest(int idUser, string name, string surname, CPF cpf, Email email, Phone phone, string passwordHash, byte userRole, byte locationId)
            : base(name, surname, cpf, email, phone, passwordHash, userRole, locationId)
        {
            Id = idUser;
        }
    }
}
