using MediatR;

namespace StockControl.API.Handlers
{
    public record LogErrorCommand(string Message, DateTime Timestamp) : IRequest;
}
