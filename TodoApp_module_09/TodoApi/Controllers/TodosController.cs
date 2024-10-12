using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.DataAccess;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings]
public class TodosController : ControllerBase
{
    private const string Anonymous = "Anonymous";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TodosController> _logger;
    private readonly ITodoData _todoData;

    public TodosController(IHttpContextAccessor httpContextAccessor, ITodoData todoData,
        ILogger<TodosController> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _todoData = todoData;
        _logger = logger;
    }

    [HttpGet("", Name = "Get all tasks")]
    public async Task<ActionResult<List<Todo.Model.Todo>>> GetAll()
    {
        _logger.LogInformation("Getting all assigned task for {User}",
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            return await _todoData.GetAllAssigned(GetUserId());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The Get all request failed for {User}",
                _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);
            return BadRequest();
        }
    }

    [HttpGet("{todoId}", Name = "Get task")]
    public async Task<ActionResult<Todo.Model.Todo>> Get(int todoId)
    {
        _logger.LogInformation("Getting task {TaskId} assigned task for {User}", todoId,
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            return await _todoData.GetOneAssigned(GetUserId(), todoId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The Get {TaskId} task failed", todoId);
            return BadRequest();
        }
    }

    [HttpPost("", Name = "Create task")]
    public async Task<ActionResult<Todo.Model.Todo>> Post([FromBody] string task)
    {
        _logger.LogInformation("Creating task for {User}",
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            var result = await _todoData.Create(GetUserId(), task);
            if (result != null)
            {
                _logger.LogInformation("Creating task for {User} completed",
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);
                return result;
            }

            _logger.LogError("Creating task for {User} failed",
                _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);
            return new BadRequestObjectResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Creation of task {Task} failed", task);
            return BadRequest();
        }
    }

    [HttpPut("{todoId}", Name = "Update task")]
    public async Task<IActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation("Creating task for {User}",
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            await _todoData.UpdateTask(GetUserId(), todoId, task);
            return new OkResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The updating task {TaskId} failed", todoId);
            return BadRequest();
        }
    }

    [HttpPut("{todoId}/complete", Name = "Complete task")]
    public IActionResult Complete(int todoId)
    {
        _logger.LogInformation("Completing task {TaskId} for {User}", todoId,
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            _todoData.CompleteTodo(GetUserId(), todoId);
            return new OkResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Setting task {TodoId} to completed failed", todoId);
            return BadRequest();
        }
    }

    [HttpDelete("{todoId}", Name = "Delete")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation("Deleting task {TaskId} for {User}", todoId,
            _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? Anonymous);

        try
        {
            await _todoData.Delete(GetUserId(), todoId);
            return new OkResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deleting {TaskId} failed", todoId);
            return BadRequest();
        }
    }

    [NonAction]
    private int GetUserId()
    {
        var userIdValue = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException();
    }
}