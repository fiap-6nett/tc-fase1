using FluentAssertions;
using Moq;
using uBee.Application.Repositories;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.ValueObjects;

namespace uBee.Application.UnitTests.Scenarios
{
    public sealed class UserTests
    {
        #region Read-Only Fields

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IPasswordHashChecker _passwordHashChecker;

        #endregion

        #region Constructor

        public UserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHashChecker = new PasswordHashChecker();
        }

        #endregion

        #region Unit Tests

        [Fact]
        public void User_Creation_Should_Succeed_With_ValidData()
        {
            // Arrange & Act
            Action action = () => CreateUser();

            // Assert
            action.Should().NotThrow<DomainException>();
        }

        [Theory]
        [InlineData(null, "Doe")]
        [InlineData("John", null)]
        public void User_Creation_Should_ThrowArgumentException_WhenNameOrSurnameIsNull(string name, string surname)
        {
            // Act
            Action action = () => new User(
                name,
                surname,
                CPF.Create("21902458001"),
                Email.Create("test@test.com"),
                Phone.Create("11-12345678"),
                "passwordHash",
                (byte)EnUserRole.Farmer,
                1);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void User_ChangePassword_Should_ThrowDomainException_WhenNewPasswordIsSameAsOld()
        {
            // Arrange
            var user = CreateUser();
            var oldPasswordHash = "hashedPassword123";

            // Act
            Action action = () => user.ChangePassword(oldPasswordHash);

            // Assert
            action.Should().Throw<DomainException>().WithMessage(DomainError.User.CannotChangePassword.Message);
        }

        [Fact]
        public void User_ChangePassword_Should_Succeed_WhenNewPasswordIsDifferent()
        {
            // Arrange
            var user = CreateUser();
            var newPasswordHash = "newHashedPassword123";

            // Act
            Action action = () => user.ChangePassword(newPasswordHash);

            // Assert
            action.Should().NotThrow<DomainException>();
            user.VerifyPasswordHash(newPasswordHash, _passwordHashChecker).Should().BeTrue();
        }

        [Fact]
        public void User_VerifyPasswordHash_Should_ReturnTrue_WhenPasswordMatches()
        {
            // Arrange
            var user = CreateUser();

            // Act
            var result = user.VerifyPasswordHash("hashedPassword123", _passwordHashChecker);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void User_VerifyPasswordHash_Should_ReturnFalse_WhenPasswordDoesNotMatch()
        {
            // Arrange
            var user = CreateUser();

            // Act
            var result = user.VerifyPasswordHash("wrongPassword", _passwordHashChecker);

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Private Methods

        private User CreateUser()
        {
            return new User(
                "John",
                "Doe",
                CPF.Create("21902458001"),
                Email.Create("john.doe@test.com"),
                Phone.Create("11-987654321"),
                "hashedPassword123",
                (byte)EnUserRole.Beekeeper,
                5);
        }

        #endregion
    }

    #region Helper Classes

    public class PasswordHashChecker : IPasswordHashChecker
    {
        public bool HashesMatch(string hashedPassword, string password) => hashedPassword == password;
    }

    #endregion
}
