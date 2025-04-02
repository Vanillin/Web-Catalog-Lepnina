using Application.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.ExceptionHandler
{
    public class ApplicationExceptionHandler(IProblemDetailsService _problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext
            , Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BaseApplicationException e)
                return false;

            httpContext.Response.StatusCode = (int)e.StatusCode;
            httpContext.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = (int)e.StatusCode,
                Title = e.Title,
                Detail = e.Message,
                Instance = httpContext.Request.Path,
                Type = e.GetType().Name
            };

            var problemDetailContext = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
                Exception = e
            };


            return true;
        }
    }
}
