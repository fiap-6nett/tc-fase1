using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Abstractions.Services;

namespace uBee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Read-Only Fields

        private readonly IUserService _userService;
        private readonly IUserSessionProvider _userSessionProvider;

        #endregion

        #region Constructors

        public UserController(IUserService userService, IUserSessionProvider userSessionProvider)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userSessionProvider = userSessionProvider ?? throw new ArgumentNullException(nameof(userSessionProvider));
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Represents the query for getting the paged list of the users.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The page size. The max page size is 100.</param>
        /// <returns>The paged list of the users.</returns>
        [HttpGet(ApiRoutes.Users.Get)]
        [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Ok(await _userService.GetUsersAsync(new GetUsersRequest(page, pageSize)));

        /// <summary>
        /// Represents the query for getting a user authenticated.
        /// </summary>
        /// <returns>The user authenticated info.</returns>
        [HttpGet(ApiRoutes.Users.GetMyProfile)]
        [ProducesResponseType(typeof(DetailedUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyProfile()
            => Ok(await _userService.GetUserByIdAsync(_userSessionProvider.IdUser));

        /// <summary>
        /// Represents the change password request.
        /// </summary>
        /// <param name="changePasswordRequest">Represents the change password request.</param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.Users.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            await _userService.ChangePasswordAsync(_userSessionProvider.IdUser, changePasswordRequest.Password);
            return Ok();
        }

        /// <summary>
        /// Represents the update user request.
        /// </summary>
        /// <param name="updateUserRequest">Represents the update user request.</param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.Users.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
        {
            await _userService.UpdateUserAsync(_userSessionProvider.IdUser, updateUserRequest.Name, updateUserRequest.Surname);
            return Ok();
        }

        #endregion
    }
}
