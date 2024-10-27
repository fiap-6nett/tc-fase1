using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Api.Infrastructure;
using uBee.Application.Authentication;
using uBee.Application.Contracts.Authentication;
using uBee.Domain.ValueObjects;

namespace uBee.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication-related endpoints.
    /// </summary>
    [AllowAnonymous]
    public sealed class AuthenticationController : ApiController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands and queries.</param>
        public AuthenticationController(IMediator mediator)
            : base(mediator)
        { }

        #endregion

        #region Endpoints

        /// <summary>
        /// Logs a user into the system by verifying their email and password.
        /// </summary>
        /// <param name="loginRequest">The login request containing email and password.</param>
        /// <returns>A <see cref="TokenResponse"/> containing the authentication token if the login is successful.</returns>
        /// <response code="200">Returns the token for the authenticated user.</response>
        /// <response code="400">Returns a bad request error if the login fails.</response>
        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await Mediator.Send(new LoginCommand(loginRequest.Email, loginRequest.Password));
            return Ok(response);
        }

        /// <summary>
        /// Registers a new user in the system with the provided details.
        /// </summary>
        /// <param name="registerRequest">The registration request containing user details.</param>
        /// <returns>A <see cref="TokenResponse"/> containing the authentication token for the registered user.</returns>
        /// <response code="200">Returns the token for the registered user.</response>
        /// <response code="400">Returns a bad request error if registration fails.</response>
        [HttpPost(ApiRoutes.Authentication.Register)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await Mediator.Send(new RegisterCommand(
                registerRequest.Name,
                registerRequest.Surname,
                CPF.Create(registerRequest.Cpf),
                Email.Create(registerRequest.Email),
                Password.Create(registerRequest.Password),
                Phone.Create(registerRequest.Phone),
                registerRequest.UserRole
            ));
            return Ok(response);
        }

        #endregion
    }
}
