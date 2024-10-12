using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace Todo.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<Model.Todo>> ExecuteWithResults(NpgsqlCommand command, string connectionStringName);
        Task ExecuteWithoutResults(NpgsqlCommand command, string connectionStringName);
    }
}