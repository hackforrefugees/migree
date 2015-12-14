using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Migree.Api.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private const string DEFAULT_ERROR_MESSAGE = "An error has occured";

        public override void Handle(ExceptionHandlerContext context)
        {
            var migreeException = context.Exception as MigreeException;
            var statusCode = migreeException?.StatusCode ?? HttpStatusCode.InternalServerError;
            var message = migreeException?.Message ?? DEFAULT_ERROR_MESSAGE;

            context.Result = new ErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = new ErrorResponse { Message = message },
                StatusCode = statusCode
            };
        }

        private class ErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }
            public ErrorResponse Content { get; set; }
            public HttpStatusCode StatusCode { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = Request.CreateResponse(StatusCode, Content);
                return Task.FromResult(response);
            }
        }
    }
}