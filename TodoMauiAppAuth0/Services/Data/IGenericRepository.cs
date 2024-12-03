namespace TodoMauiAppAuth0.Services.Data;
public interface IGenericRepository
{
    Task<T> GetAsync<T>(Uri uri);
    Task PostAsync<T>(Uri uri, T data);
    Task<R> PostAsync<T, R>(Uri uri, T data);
    Task<T> PutAsync<T>(Uri uri, T data);
    Task DeleteAsync(Uri uri);
}