using System.Net.Http.Json;

namespace LottoGenWeb.Services;

public class HttpClientWrapper(HttpClient client) : IHttpClientWrapper
{
    private readonly HttpClient _client = client;

    public Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken = default)
    {
        return _client.PostAsJsonAsync(requestUri, value, cancellationToken);
    }
}