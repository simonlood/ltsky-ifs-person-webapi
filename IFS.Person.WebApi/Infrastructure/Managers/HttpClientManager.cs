using Microsoft.Extensions.Options;
using IFS.Person.WebApi.Infrastructure.Managers.Interfaces;
using System.Net;

namespace IFS.Person.WebApi.Infrastructure.Managers;

public class HttpClientManager : IHttpClientManager
{
    private readonly AppSettings _appSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientManager(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _appSettings = options.Value;
    }

    public HttpClient GetHttpClient(string httpClientName)
    {
        var httpClient = _httpClientFactory.CreateClient(httpClientName);
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Authorization", _appSettings.IFSBasicAuth);
        httpClient.BaseAddress = new Uri(_appSettings.IFSUri);

        return httpClient;
    }
}