using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Todo.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(IConfiguration configuration, ILogger<SqlDataAccess> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Model.Todo>> ExecuteWithResults(NpgsqlCommand command, string connectionStringName)
        {
            _logger.LogInformation("Loading data from SQL Script {Script}", command.CommandText);

            var connectionString = _configuration.GetConnectionString(connectionStringName);
            await using var dbConnection = new NpgsqlConnection(connectionString);
            command.Connection = dbConnection;  

            await dbConnection.OpenAsync().ConfigureAwait(false);
            await using var reader = await command.ExecuteReaderAsync();
            var list = await ReadTodoAsync(reader);
            await dbConnection.CloseAsync().ConfigureAwait(false);
                    
            return list;
        }
    
        public async Task ExecuteWithoutResults(NpgsqlCommand command, string connectionStringName)
        {
            _logger.LogInformation("Loading data from SQL Script {Script} from connection {ConnectionStringName}",
                command.CommandText, connectionStringName);
        
            var connectionString = _configuration.GetConnectionString(connectionStringName);
            await using var dbConnection = new NpgsqlConnection(connectionString);
            command.Connection = dbConnection;

            await dbConnection.OpenAsync().ConfigureAwait(false);
            var result = await command.ExecuteNonQueryAsync();
            await dbConnection.CloseAsync().ConfigureAwait(false);
            
            _logger.LogInformation("Updated {NumberOfRecords} records", result);
        }
        
        private static async Task<List<Model.Todo>> ReadTodoAsync(DbDataReader reader)
        {
            var list = new List<Model.Todo>();
            while (await reader.ReadAsync())
            {
                var todo = GetTodo(reader);
                if (todo != null)
                {
                    list.Add(todo);
                }
            }
            
            return list;
        }

        private static Model.Todo GetTodo(DbDataReader reader)
        {
            var todo = new Model.Todo
            {
                Id = reader.GetInt32("Id"),
                IsComplete = reader.GetBoolean("IsComplete"),
                Task = reader.GetString("Task"),
                AssignedTo = reader.GetInt32("AssignedTo")
            };
            return todo;
        }

    }
}