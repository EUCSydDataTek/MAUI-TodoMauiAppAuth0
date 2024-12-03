using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace TodoMauiAppAuth0.Services.Data;
public class GenericRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger<GenericRepository> _logger;


    #region CONSTRUCTOR
    public GenericRepository(HttpClient httpClient, ILogger<GenericRepository> logger)
    {
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _logger = logger;
    }
    #endregion

    #region GET
    public async Task<T> GetAsync<T>(Uri uri)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(jsonContent, _serializerOptions)!;
            }
            else
            {
                _logger.LogError($"Error in GetAsync: {response.ReasonPhrase}");
                return default!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in GetAsync: {ex.Message}");
            throw;
        }
    }
    #endregion

    #region POST
    public async Task PostAsync<T>(Uri uri, T data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await _httpClient.PostAsync(uri, content).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in PostAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<R> PostAsync<T, R>(Uri uri, T data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<R>(jsonContent, _serializerOptions)!;
            }
            else
            {
                _logger.LogError($"Error in PostAsync: {response.ReasonPhrase}");
                return default!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in PostAsync: {ex.Message}");
            throw;
        }
    }
    #endregion

    #region PUT
    public async Task<T> PutAsync<T>(Uri uri, T data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in PutAsync: {ex.Message}");
            throw;
        }
    }
    #endregion

    #region DELETE
    public async Task DeleteAsync(Uri uri)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in DeleteAsync: {ex.Message}");
            throw;
        }
    }
    #endregion

}