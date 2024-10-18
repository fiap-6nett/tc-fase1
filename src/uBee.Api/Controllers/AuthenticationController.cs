using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Api.Infrastructure;
using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Abstractions.Services;

namespace uBee.Api.Controllers
{
    [AllowAnonymous]
    public sealed class AuthenticationController : ApiController
    {
        #region Read-Only Fields

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors

        public AuthenticationController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        #endregion

        #region Endpoints

        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RegisterRequest registerRequest)
        {
            var response = await _userService.CreateAsync(registerRequest.Name, registerRequest.Surname, registerRequest.Email, registerRequest.Password, registerRequest.UserRole, registerRequest.Location, registerRequest.Phone);
            return Ok(response);
        }

        #endregion
    }
}
