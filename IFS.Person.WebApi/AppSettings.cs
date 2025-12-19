namespace IFS.Person.WebApi;

public class AppSettings
{
    public string DomainApiKey { get; set; }
    public string IFSBasicAuth { get; set; }
    public string IFSUri { get; set; }
    public bool IsImpersonated { get; set; }
}