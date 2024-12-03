using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using System.Text.Json;

namespace TodoMauiAppAuth0.Services.AuthServices;
public static class Auth0ClientExtensions
{
    public static Task<Tokens> ToTokens(this LoginResult loginResult)
        => new Tokens
        {
            AccessToken = loginResult.AccessToken,
            IdentityToken = loginResult.IdentityToken,
            RefreshToken = loginResult.RefreshToken,
            AccessTokenExpiration = loginResult.AccessTokenExpiration.UtcDateTime
        }.ToSecureStorage();

    public static Task<Tokens> ToTokens(this RefreshTokenResult refreshTokenResult)
        => new Tokens
        {
            AccessToken = refreshTokenResult.AccessToken,
            IdentityToken = refreshTokenResult.IdentityToken,
            RefreshToken = refreshTokenResult.RefreshToken, // Only used when RefreshToken is rotated
            AccessTokenExpiration = refreshTokenResult.AccessTokenExpiration.UtcDateTime
        }.ToSecureStorage();

    public async static Task<Tokens> ToSecureStorage(this Tokens tokens)
    {
        // Serialize and save to Secure Storage
        await SecureStorage.Default.SetAsync(nameof(AuthService.TokensSecureStorage), JsonSerializer.Serialize(tokens));
        return tokens;
    }


    public async static Task<UserProfile> ToUserProfile(this LoginResult loginResult)
    {
        UserProfile userProfile = new()
        {
            UserName = loginResult.User.FindFirst(c => c.Type == "name")?.Value,
            Email = loginResult.User.FindFirst(c => c.Type == "email")?.Value,
            ProfilePictureUrl = loginResult.User.FindFirst(c => c.Type == "picture")?.Value
        };
        // Serialize and save to Secure Storage
        await SecureStorage.Default.SetAsync(nameof(AuthService.UserProfileSecureStorage), JsonSerializer.Serialize(userProfile));
        return userProfile;
    }
}
