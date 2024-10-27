using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBee.Api.Constants;
using uBee.Api.Contracts;
using uBee.Domain.Core.Primitives;

namespace uBee.Api.Infrastructure
{
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApiController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #region Methods

        protected new IActionResult Ok(object value)
            => base.Ok(value);

        protected IActionResult BadRequest(Error error)
            => BadRequest(new ApiErrorResponse(error));

        protected new IActionResult NotFound()
            => NotFound(Errors.NotFoudError.Message);

        #endregion
    }
}
