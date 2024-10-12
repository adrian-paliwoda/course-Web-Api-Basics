using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.DataAccess
{
    public interface ITodoData
    {
        Task<List<Model.Todo>> GetAllAssigned(int assignedTo);
        Task<Model.Todo> GetOneAssigned(int assignedTo, int todoId);
        Task<Model.Todo> Create(int assignedTo, string task);
        Task UpdateTask(int assignedTo, int todoId, string task);
        Task CompleteTodo(int assignedTo, int todoId);
        Task Delete(int assignedTo, int todoId);
    }
}