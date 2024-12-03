using TodoMauiAppAuth0.Models;

namespace TodoMauiAppAuth0.Services.Data;
public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
    Task<TodoItem> GetTodoItemAsync(int id);
    Task<TodoItem> MarkItemCompleted(TodoItem todoItem);
    Task CreateNewTodoItemAsync(TodoItem todoItem);
    Task<TodoItem> EditTodoItemAsync(TodoItem todoItem);
    Task DeleteItem(int id);
}