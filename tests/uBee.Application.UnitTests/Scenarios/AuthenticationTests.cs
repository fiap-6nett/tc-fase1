using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using uBee.Application.Authentication;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Abstractions.Cryptography;
using uBee.Application.Core.Abstractions.Data;
using uBee.Application.Repositories;
using uBee.Application.UnitTests.TestEntities;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.ValueObjects;
using uBee.Infrastructure.Authentication;
using uBee.Infrastructure.Authentication.Settings;
using uBee.Infrastructure.Cryptography;

namespace uBee.Application.UnitTests.Scenarios
{
    public sealed class AuthenticationTests
    {
        #region Read-Only Fields

        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordHashChecker _passwordHashChecker;

        private readonly Mock<IDbContext> _dbContextMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILocationRepository> _locationRepositoryMock;

        #endregion

        #region Constructor

        public AuthenticationTests()
        {
            _jwtProvider = new JwtProvider(JwtOptions);
            _passwordHasher = new PasswordHasher();
            _passwordHashChecker = new PasswordHasher();

            _dbContextMock = new Mock<IDbContext>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _locationRepositoryMock = new Mock<ILocationRepository>();
        }

        #endregion

        #region Unit Tests

        #region Login

        [Fact]
        public async Task Login_Should_ReturnTokenResponseAsync_WithValidCredentials()
        {
            // Arrange
            var user = GetUser();
            var email = Email.Create("john.doe@test.com");
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            var handler = new LoginCommandHandler(_jwtProvider, _userRepositoryMock.Object, _passwordHashChecker);
            var loginCommand = new LoginCommand("john.doe@test.com", "John@123");

            // Act
            var result = await handler.Handle(loginCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Login_Should_ThrowDomainException_WithInvalidEmail()
        {
            // Arrange
            var email = Email.Create("johndoe@test.com");
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((User)null);

            var handler = new LoginCommandHandler(_jwtProvider, _userRepositoryMock.Object, _passwordHashChecker);
            var loginCommand = new LoginCommand("johndoe@test.com", "John@123");

            // Act
            var action = async () => await handler.Handle(loginCommand, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<DomainException>()
                .WithMessage(DomainError.Authentication.EmailNotRegistered.Message);
        }

        [Fact]
        public async Task Login_Should_ThrowDomainException_WithInvalidPassword()
        {
            // Arrange
            var email = Email.Create("john.doe@test.com");
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(GetUser());

            var handler = new LoginCommandHandler(_jwtProvider, _userRepositoryMock.Object, _passwordHashChecker);
            var loginCommand = new LoginCommand("john.doe@test.com", "InvalidPassword");

            // Act
            var action = async () => await handler.Handle(loginCommand, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<DomainException>()
                .WithMessage(DomainError.Authentication.InvalidEmailOrPassword.Message);
        }

        #endregion

        #region Register

        [Fact]
        public async Task Register_Should_ThrowDomainException_WhenEmailIsNotUnique()
        {
            // Arrange
            var email = Email.Create("john.doe@test.com");
            _userRepositoryMock.Setup(x => x.IsEmailUniqueAsync(email)).ReturnsAsync(false);

            var handler = new RegisterCommandHandler(_jwtProvider, _userRepositoryMock.Object, _locationRepositoryMock.Object, _passwordHasher, _unitOfWorkMock.Object);
            var registerCommand = new RegisterCommand("John", "Doe", CPF.Create("10023178000"), email, "John@123", Phone.Create("15-20347522"), EnUserRole.Administrator);

            // Act
            var action = async () => await handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<DomainException>()
                .WithMessage(DomainError.User.EmailUnavailable.Message);
        }

        [Fact]
        public async Task Register_Should_ThrowDomainException_WhenCpfIsNotUnique()
        {
            // Arrange
            var cpf = CPF.Create("10023178000");
            var email = Email.Create("john.doe@test.com");

            _userRepositoryMock.Setup(x => x.IsCpfUniqueAsync(cpf)).ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.IsEmailUniqueAsync(email)).ReturnsAsync(true);

            var handler = new RegisterCommandHandler(_jwtProvider, _userRepositoryMock.Object, _locationRepositoryMock.Object, _passwordHasher, _unitOfWorkMock.Object);
            var registerCommand = new RegisterCommand("John", "Doe", cpf, email, "John@123", Phone.Create("15-20347522"), EnUserRole.Administrator);

            // Act
            var action = async () => await handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<DomainException>()
                .WithMessage(DomainError.User.DuplicateCpf.Message);
        }

        #endregion

        #endregion

        #region Private Methods

        private UserTest GetUser() => new UserTest(
            idUser: 10_000,
            name: "John",
            surname: "Doe",
            cpf: CPF.Create("10023178000"),
            email: Email.Create("john.doe@test.com"),
            phone: Phone.Create("15-20347522"),
            passwordHash: _passwordHasher.HashPassword(Password.Create("John@123")),
            userRole: 1,
            locationId: 5
        );

        private IOptions<JwtSettings> JwtOptions => Options.Create(new JwtSettings
        {
            Issuer = "http://localhost",
            Audience = "http://localhost",
            SecurityKey = "f143bfc760543ec317abd4e8748d9f2b44dfb07a",
            TokenExpirationInMinutes = 60
        });

        #endregion
    }
}
