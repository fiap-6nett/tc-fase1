using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Contracts;
using uBee.Api.Infrastructure;
using uBee.Application.Authentication;
using uBee.Application.Contracts.Authentication;

namespace uBee.Api.Controllers
{
    [AllowAnonymous]
    public sealed class AuthenticationController : ApiController
    {
        #region Constructors

        public AuthenticationController(IMediator mediator)
            : base(mediator)
        { }

        #endregion

        #region Endpoints

        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await Mediator.Send(new LoginCommand(loginRequest.Email, loginRequest.Password));
            return Ok(response);
        }

        #endregion
    }
}
