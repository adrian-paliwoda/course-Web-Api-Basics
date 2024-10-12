using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.DataAccess;

namespace MinimalApi.Endpoints;

public static class TodoEndpoints
{
    public static void AddTodosEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/todos", GetAll);
        app.MapPost("/api/todos", CreateTask).RequireAuthorization();
        app.MapDelete("/api/todos/{id}", DeleteTask).RequireAuthorization();
    }

    [Authorize]
    private static async Task<IResult> GetAll(ITodoData todoData)
    {
        var output = await todoData.GetAllAssigned(1);
        return Results.Ok(output);
    }

    private static async Task<IResult> CreateTask(ITodoData todoData, [FromBody] string task)
    {
        var output = await todoData.Create(1, task);
        return Results.Ok(output);
    }

    private static async Task DeleteTask(ITodoData todoData, int id)
    {
        await todoData.Delete(1, id);
    }
}