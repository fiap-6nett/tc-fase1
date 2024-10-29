using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Api.Infrastructure;
using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Users;

namespace uBee.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling user-related endpoints.
    /// </summary>
    public sealed class UsersController : ApiController
    {
        #region Read-Only Fields

        private readonly IUserSessionProvider _userSessionProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands and queries.</param>
        public UsersController(IMediator mediator, IUserSessionProvider userSessionProvider)
            : base(mediator)
        {
            _userSessionProvider = userSessionProvider ?? throw new ArgumentNullException(nameof(userSessionProvider));
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Retrieves a paginated list of users filtered by DDD (area code).
        /// </summary>
        /// <param name="ddd">The DDD (area code) to filter users by.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of users to return per page.</param>
        /// <returns>A paginated list of users filtered by DDD.</returns>
        /// <response code="200">Returns the paginated list of users.</response>
        /// <response code="400">Returns a bad request error if the request fails.</response>
        [HttpGet(ApiRoutes.Users.Get)]
        [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int ddd, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await Mediator.Send(new GetUsersQuery(ddd, page, pageSize));
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the current logged-in user's details.
        /// </summary>
        /// <returns>The user's details.</returns>
        /// <response code="200">Returns the details of the logged-in user.</response>
        /// <response code="400">Returns a bad request error if the operation fails.</response>
        [HttpGet(ApiRoutes.Users.GetMyProfile)]
        [ProducesResponseType(typeof(DetailedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyProfile()
        {
            var response = await Mediator.Send(new GetUserByIdQuery(_userSessionProvider.IdUser));
            return Ok(response);
        }

        /// <summary>
        /// Changes the password for the currently logged-in user.
        /// </summary>
        /// <param name="changePasswordRequest">The request containing the current and new passwords.</param>
        /// <returns>Returns No Content (204) if the password was successfully changed.</returns>
        /// <response code="204">Password was changed successfully.</response>
        /// <response code="400">Bad request error if the operation fails.</response>
        [HttpPut(ApiRoutes.Users.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            await Mediator.Send(new UpdateUserPasswordCommand(_userSessionProvider.IdUser, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword));
            return NoContent();
        }

        /// <summary>
        /// Soft deletes the currently logged-in user.
        /// </summary>
        /// <returns>Returns No Content (204) if the user was deleted successfully.</returns>
        /// <response code="204">User was deleted successfully.</response>
        /// <response code="400">Returns a bad request error if the deletion fails.</response>
        [Authorize] // Ensures only logged-in users can delete their account
        [HttpDelete(ApiRoutes.Users.DeleteMyProfile)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMyProfile()
        {
            await Mediator.Send(new DeleteUserCommand(_userSessionProvider.IdUser));
            return NoContent();
        }


        #endregion
    }
}
