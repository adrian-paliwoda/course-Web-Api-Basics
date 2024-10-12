using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Todo.DataAccess
{
    public class TodoData : ITodoData
    {
        private const string Default = "Default";
        private readonly string _connectionStringName;
        private readonly ISqlDataAccess _sqlDataAccess;

        public TodoData(ISqlDataAccess sqlDataAccess, string connectionStringName = Default)
        {
            _sqlDataAccess = sqlDataAccess;
            _connectionStringName = connectionStringName;
        }

        public async Task<List<Model.Todo>> GetAllAssigned(int assignedTo)
        {
            var command = new NpgsqlCommand("SELECT * FROM spTodos_GetAllAssigned(@1)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1", assignedTo)
                }
            };
            
            return await _sqlDataAccess.ExecuteWithResults(command, _connectionStringName);
        }


        public async Task<Model.Todo> GetOneAssigned(int assignedTo, int todoId)
        {
            var command = new NpgsqlCommand("SELECT * FROM sptodos_getoneassigned(@1, @2)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1", assignedTo),
                    new NpgsqlParameter("@2", todoId)
                }
            };
            
                return (await _sqlDataAccess.ExecuteWithResults(command, _connectionStringName)).FirstOrDefault();
        }

        public async Task<Model.Todo> Create(int assignedTo, string task)
        {
            var command = new NpgsqlCommand("SELECT * FROM spTodos_Create(@1, @2)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1", task),
                    new NpgsqlParameter("@2", assignedTo)
                }
            };
            
            return (await _sqlDataAccess.ExecuteWithResults(command, _connectionStringName)).LastOrDefault();
        }
    
        public async Task UpdateTask(int assignedTo, int todoId, string task)
        {
            var command = new NpgsqlCommand("CALL spTodos_UpdateTask(@1, @2, @3)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1 ", task),
                    new NpgsqlParameter("@2", assignedTo),
                    new NpgsqlParameter("@3", todoId)
                }
            };
            
            await _sqlDataAccess.ExecuteWithoutResults(command, _connectionStringName);
        }

        public async Task CompleteTodo(int assignedTo, int todoId)
        {
            var command = new NpgsqlCommand("CALL spTodos_CompleteTodo(@1, @2)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1", assignedTo),
                    new NpgsqlParameter("@2", todoId),
                }
            };
            
            await _sqlDataAccess.ExecuteWithoutResults(command, _connectionStringName);
        }

        public async Task Delete(int assignedTo, int todoId)
        {
            var command = new NpgsqlCommand("CALL spTodos_Delete(@1, @2)")
            {
                Parameters =
                {
                    new NpgsqlParameter("@1", assignedTo),
                    new NpgsqlParameter("@2", todoId),
                }
            };
            
            await _sqlDataAccess.ExecuteWithoutResults(command, _connectionStringName);
        }
    }
}