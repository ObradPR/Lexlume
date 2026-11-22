using Lexlume.Application.Exceptions;
using Lexlume.Common.Extensions;
using Lexlume.Domain.Common;
using Lexlume.Domain.Interfaces;
using System.Net;

namespace Lexlume.Web.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, IExceptionLogger logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException)
            when (context.RequestAborted.IsCancellationRequested)
        {
            // The client disconnected or cancelled the request.
            // There is no need to treat this as an unexpected error.
        }
        catch (Exception ex)
        {
            await logger.LogExceptionAsync(ex);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context, Exception ex)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.StatusCode =
            (int)HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var response = new ResponseWrapper<object>();

        if (ex is FluentValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            response.Code = HttpStatusCode.UnprocessableEntity;
            response.Errors = validationException.Failures
                .SelectMany(kvp => kvp.Value.Select(msg => new Error(kvp.Key, msg)))
                .ToList();
        }
        else
        {
            response.AddError(new("Error", "An unexpected error occured."), (HttpStatusCode)context.Response.StatusCode);
        }

        await context.Response.WriteAsync(response.SerializeWebJson());
    }
}