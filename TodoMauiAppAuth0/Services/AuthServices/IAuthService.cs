using IdentityModel.OidcClient.Browser;

namespace TodoMauiAppAuth0.Services.AuthServices;
public interface IAuthService
{
    public UserProfile UserProfile { get; }
    public Tokens Credentials { get; }
    public Task<string> GetAccessTokenAsync();

    public Task<bool> AuthenticateAsync();
    public Task LogoutAsync();
}
