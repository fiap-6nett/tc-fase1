using System.Data;
using Microsoft.EntityFrameworkCore;
using uBee.Application.Contracts.Authentication;
using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Abstractions.Cryptography;
using uBee.Application.Core.Abstractions.Data;
using uBee.Application.Core.Abstractions.Services;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Domain.ValueObjects;

namespace uBee.Application.Services
{
    internal sealed class UserService : IUserService
    {
        #region Read-Only Fields

        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        #endregion

        #region Constructors

        public UserService(IDbContext dbContext,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
        )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        #endregion

        #region IUserService Members

        public async Task<TokenResponse> CreateAsync(string name, string surname, string email, string password, EnUserRole userRole, EnLocation location, string phone)
        {
            if (!await _userRepository.IsEmailUniqueAsync(email))
                throw new DomainException(DomainError.User.DuplicateEmail);

            var passwordHash = _passwordHasher.HashPassword(Password.Create(password));
            var user = new User(name, surname, email, phone, passwordHash, userRole, location);

            await _userRepository.InsertAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new TokenResponse(_jwtProvider.Create(user));
        }

        public async Task ChangePasswordAsync(Guid idUser, string password)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user is null)
                throw new DomainException(DomainError.User.NotFound);

            var passwordHash = _passwordHasher.HashPassword(Password.Create(password));
            user.ChangePassword(passwordHash);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Guid idUser, string name, string surname)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user is null)
                throw new DomainException(DomainError.User.NotFound);

            user.ChangeName(name, surname);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DetailedUserResponse> GetUserByIdAsync(Guid idUser)
        {
            var userQuery = (
                from user in _dbContext.Set<User, Guid>().AsNoTracking()
                where user.Id == idUser
                select new DetailedUserResponse
                {
                    IdUser = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    UserRole = user.UserRole,
                    Location = user.Location,
                    CreatedAt = user.CreatedAt,
                    LastUpdatedAt = user.LastUpdatedAt
                }
            );

            return await userQuery.FirstOrDefaultAsync();
        }

        public async Task<PagedList<UserResponse>> GetUsersAsync(GetUsersRequest request)
        {
            IQueryable<UserResponse> usersQuery = (
                from user in _dbContext.Set<User, Guid>().AsNoTracking()
                orderby user.Name
                select new UserResponse
                {
                    IdUser = user.Id,
                    FullName = $"{user.Name} {user.Surname}",
                    FullPhone = $"{user.Location} {user.Phone}",
                    UserRole = user.UserRole,
                }
            );

            var totalCount = await usersQuery.CountAsync();

            var usersReponsePage = await usersQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync();

            return new PagedList<UserResponse>(usersReponsePage, request.Page, request.PageSize, totalCount);
        }

        #endregion
    }
}
