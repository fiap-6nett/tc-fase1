using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Application.Contracts.Users;
using uBee.Application.Users;
using uBee.Application.Contracts.Common;
using uBee.Api.Infrastructure;

namespace uBee.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling user-related endpoints.
    /// </summary>
    [AllowAnonymous]
    public sealed class UsersController : ApiController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands and queries.</param>
        public UsersController(IMediator mediator)
            : base(mediator)
        { }

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
        [HttpGet(ApiRoutes.Users.List)]
        [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListUsers([FromQuery] int ddd, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await Mediator.Send(new ListUsersQuery(ddd, page, pageSize));
            return Ok(response);
        }

        #endregion
    }
}
