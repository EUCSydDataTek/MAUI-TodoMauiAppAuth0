using TodoMauiAppAuth0.Models;

namespace TodoMauiAppAuth0.Services.Data;
public class TodoItemService(GenericRepository repository) : ITodoItemService
{
    private readonly GenericRepository _repository = repository;

    private Uri BuildUri(string path)
    {
        return new UriBuilder(Config.BaseAddress) { Path = path }.Uri;
    }

    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        var uri = BuildUri(Config.TodoitemsEndpoint);
        return await _repository.GetAsync<IEnumerable<TodoItem>>(uri).ConfigureAwait(false);
    }

    public async Task<TodoItem> GetTodoItemAsync(int id)
    {
        var uri = BuildUri($"{Config.TodoitemsEndpoint}/{id}");
        return await _repository.GetAsync<TodoItem>(uri).ConfigureAwait(false);
    }

    public async Task<TodoItem> MarkItemCompleted(TodoItem todoItem)
    {
        var uri = BuildUri($"{Config.TodoitemsEndpoint}/{todoItem.Id}");
        return await _repository.PutAsync(uri, todoItem).ConfigureAwait(false);
    }

    public async Task CreateNewTodoItemAsync(TodoItem todoItem)
    {
        var uri = BuildUri(Config.TodoitemsEndpoint);
        await _repository.PostAsync(uri, todoItem).ConfigureAwait(false);
    }

    public async Task<TodoItem> EditTodoItemAsync(TodoItem todoItem)
    {
        var uri = BuildUri($"{Config.TodoitemsEndpoint}/{todoItem.Id}");
        return await _repository.PutAsync(uri, todoItem).ConfigureAwait(false);
    }

    public async Task DeleteItem(int id)
    {
        var uri = BuildUri($"{Config.TodoitemsEndpoint}/{id}");
        await _repository.DeleteAsync(uri).ConfigureAwait(false);
    }
}
