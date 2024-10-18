using System.Text.Json.Serialization;
using uBee.Domain.Core.Primitives;

namespace uBee.Api.Contracts
{
    /// <summary>
    /// Represents API an error response.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        public IReadOnlyCollection<Error> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiErrorResponse"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ApiErrorResponse(params Error[] errors)
            => Errors = errors;

        [JsonConstructor]
        public ApiErrorResponse(IReadOnlyCollection<Error> errors)
            => Errors = errors;
    }
}
