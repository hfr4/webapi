namespace TodoApi;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet(Name = "GetTodos")]
    public TodoModel Get()
    {
        return  new TodoModel(); 
    }
}
