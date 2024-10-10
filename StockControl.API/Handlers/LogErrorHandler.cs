using Dapper;
using MediatR;
using StockControl.API.Repositories;

namespace StockControl.API.Handlers
{
    public class LogErrorHandler : IRequestHandler<LogErrorCommand>
    {
        private readonly DataContext _dataContext;

        public LogErrorHandler(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task Handle(LogErrorCommand request, CancellationToken cancellationToken)
        {
            using var _connection = _dataContext.CreateConnection();
            var query = "INSERT INTO ErrorLogs (Message, Timestamp) VALUES (@Message, @Timestamp)";
            var parameters = new { request.Message, request.Timestamp };

            await _connection.ExecuteAsync(query, parameters);
        }
    }
}
