namespace TodoBlazorApp.Model;

public class Todo
{
    public int Id { get; set; }
    public string? Task { get; set; }
    public int AssignedTo { get; set; }
    public bool IsComplete { get; set; }
}