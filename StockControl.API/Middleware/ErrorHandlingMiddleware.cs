using MediatR;
using StockControl.API.Handlers;

namespace StockControl.API.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, IMediator mediator)
    {
        private readonly RequestDelegate _next = next;
        private readonly IMediator _mediator = mediator;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
