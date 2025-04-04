using Application.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandler
{
#pragma warning disable CS9113 // Параметр не прочитан.
#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
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
#pragma warning restore CS9113 // Параметр не прочитан.
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
}
