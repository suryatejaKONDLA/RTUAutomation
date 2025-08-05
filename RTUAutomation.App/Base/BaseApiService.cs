namespace RTUAutomation.App.Base;

public class BaseApiService
{
#region GET

    public async Task<T> GetAsync<T>(string url, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<T>();
    }

#endregion

#region Send Request

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool includeAuthHeader)
    {
        try
        {
            if (includeAuthHeader)
            {
                //await AttachBearerTokenAsync(request);
            }

            var stopwatch = Stopwatch.StartNew();

            var requestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : "";
            long requestSize = requestBody.Length;

            var response = await HttpClient.SendAsync(request);

            stopwatch.Stop();

            var responseBody = await response.Content.ReadAsStringAsync();
            long responseSize = responseBody.Length;

            Log.Information("HTTP {Method} {Url} completed in {Elapsed} ms | Req: {ReqBytes} bytes ({ReqMB:N3} MB) | Res: {ResBytes} bytes ({ResMB:N3} MB)",
                request.Method,
                request.RequestUri,
                stopwatch.Elapsed.TotalMilliseconds,
                requestSize,
                requestSize / (1024.0 * 1024.0),
                responseSize,
                responseSize / (1024.0 * 1024.0)
            );

            if (HandleRedirects(response))
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return response;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "HTTP {Method} {Url} failed", request.Method, request.RequestUri);
            throw;
        }
    }

#endregion

#region Fields and Constructor

    private readonly HttpClient HttpClient;
    private readonly NavigationManager Navigation;

    public BaseApiService(HttpClient httpClient, ApiContext context, NavigationManager navigation)
    {
        HttpClient = httpClient;
        Navigation = navigation;

        if (HttpClient.BaseAddress == null)
        {
            HttpClient.BaseAddress = new(context.ApiBaseUrl);
        }
    }

#endregion

#region POST

    public async Task<TResponse> PostAsync<TResponse>(string url, object data, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

#endregion

#region PUT

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse> PutAsync<TResponse>(string url, object data, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

#endregion

#region DELETE

    public async Task<TResponse> DeleteAsync<TResponse>(string url, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string url, TRequest data, bool includeAuthHeader = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await SendAsync(request, includeAuthHeader);
        return response == null ? default : await response.Content.ReadFromJsonAsync<TResponse>();
    }

#endregion

#region Helpers

    private bool HandleRedirects(HttpResponseMessage response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
                Navigation.NavigateTo("401", true);
                return true;
            case HttpStatusCode.Forbidden:
                Navigation.NavigateTo("403", true);
                return true;
            case HttpStatusCode.NotFound:
                Navigation.NavigateTo("404", true);
                return true;
            default:
                return false;
        }
    }

    //private async Task AttachBearerTokenAsync(HttpRequestMessage request)
    //{
    //    var token = await TokenProvider.GetTokenAsync();
    //    if (!string.IsNullOrWhiteSpace(token))
    //    {
    //        request.Headers.Authorization = new("Bearer", token);
    //    }
    //}

#endregion
}