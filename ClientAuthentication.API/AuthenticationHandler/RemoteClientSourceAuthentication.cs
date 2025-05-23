namespace ClientAuthentication.API.AuthenticationHandler;

public class RemoteClientSourceAuthentication : IClientSourceAuthenticationHandler
{
    private static readonly HttpClient _httpClient = new();
    private string _authenticationServiceUrl;

    public RemoteClientSourceAuthentication(string authenticationServiceUrl)
    {
        _authenticationServiceUrl = authenticationServiceUrl;
    }

    public bool Validate(string clientSource)
    {
        if (string.IsNullOrEmpty(clientSource))
        {
            return false;
        }
        var response = _httpClient.GetAsync($"{_authenticationServiceUrl}/api/clientsource/{clientSource}").Result;
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }
}