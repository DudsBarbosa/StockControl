using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace StockControl.API.Repositories
{
    public class DataContext(IOptions<DbSettings> dbSettings)
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
            return new MySqlConnection(connectionString);
        }

        public async Task Init()
        {
            await InitDatabase();
            await InitTables();
        }

        private async Task InitDatabase()
        {
            // create database if it doesn't exist
            var connectionString = $"Server={_dbSettings.Server}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
            using var connection = new MySqlConnection(connectionString);
            var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
            await connection.ExecuteAsync(sql);
        }

        private async Task InitTables()
        {
            // create tables if they don't exist
            using var connection = CreateConnection();
            await _initProducts();

            async Task _initProducts()
            {
                var sql = """
                CREATE TABLE IF NOT EXISTS Products (
                    Id INT NOT NULL AUTO_INCREMENT,
                    Description VARCHAR(255) NOT NULL,
                    StockQuantity INT,
                    AverageCostPrice DECIMAL,                    
                    PartNumber VARCHAR(255),
                    PRIMARY KEY (Id)
                );

                CREATE TABLE IF NOT EXISTS Orders (
                    Id INT NOT NULL AUTO_INCREMENT,
                    ProductId INT NOT NULL,
                    Date DATE,
                    Quantity INT,
                    Value DECIMAL,
                    PRIMARY KEY (Id)
                );

                CREATE TABLE IF NOT EXISTS ErrorLogs (
                    Id INT NOT NULL AUTO_INCREMENT,
                    Message TEXT,
                    Timestamp DATETIME,
                    PRIMARY KEY (Id)
                );
            """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
