namespace TodoMauiAppAuth0.Services.AuthServices;
public class Tokens
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? AccessTokenExpiration { get; set; }

    public string? IdentityToken { get; set; }
}