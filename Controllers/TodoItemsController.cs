using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemsController : ControllerBase
{
    private static List<TodoItem> todoItems = new List<TodoItem>
    {
            new TodoItem { Id = 1, Name = "Task 1", IsComplete = true },
            new TodoItem { Id = 2, Name = "Task 2", IsComplete = false},
            new TodoItem { Id = 3, Name = "Task 3", IsComplete = true }
    };

    private readonly ILogger<TodoItemsController> _logger;

    public TodoItemsController(ILogger<TodoItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAllTodos")]
    public IActionResult Get()
    {
        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todoItem = todoItems.Find(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

    [HttpPost]
    public IActionResult Create(TodoItem todoItem)
    {
        todoItem.Id = todoItems.Count + 1;
        todoItems.Add(todoItem);
        return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TodoItem updatedTodoItem)
    {
        var todoItem = todoItems.Find(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = updatedTodoItem.Name;
        todoItem.IsComplete = updatedTodoItem.IsComplete;
        return NoContent();
    }

    [HttpDelete(Name = "DeleteTodo")]
    public IActionResult Delete(int id)
    {
        var todoItem = todoItems.Find(
        item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        todoItems.Remove(todoItem);
        return NoContent();
    }
}