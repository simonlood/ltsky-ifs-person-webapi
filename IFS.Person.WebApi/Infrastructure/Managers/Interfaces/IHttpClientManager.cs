namespace IFS.Person.WebApi.Infrastructure.Managers.Interfaces;

public interface IHttpClientManager
{
    HttpClient GetHttpClient(string httpClientName);
}