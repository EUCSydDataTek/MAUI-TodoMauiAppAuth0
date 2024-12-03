using Auth0.OidcClient;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace TodoMauiAppAuth0.Services.AuthServices;
public partial class AuthService(Auth0Client _auth0Client, ILogger<AuthService> _logger)
{
    // Secure Storage names
    public const string TokensSecureStorage = "TokensSecureStorage";
    public const string RefreshTokenSecureStorage = "RefreshTokenSecureStorage";
    public const string UserProfileSecureStorage = "UserProfileSecureStorage";

    public UserProfile? UserProfile { get; private set; }
    public Tokens? Tokens { get; private set; }
    private LoginResult? _loginResult;

    public bool IsUserProfileCreated => SecureStorage.GetAsync(nameof(UserProfileSecureStorage)).Result is not null;

    public async Task<bool> AuthenticateAsync()
    {
        _loginResult = await _auth0Client.LoginAsync(new { audience = Config.Audience });

        if (!_loginResult.IsError)
        {
            // Save the tokens and the user profile to Secure Storage
            Tokens = await _loginResult.ToTokens();
            UserProfile = await _loginResult.ToUserProfile();
            _logger.LogInformation($"********************************** 3. Authenticate with AccessToken is valid to: {Tokens.AccessTokenExpiration} ***********");
            _logger.LogInformation($"********************************** 3. Authenticate with Access Token: {Tokens.AccessToken} ***********");
            _logger.LogInformation($"********************************** 3. Authenticate with Refresh Token: {Tokens.RefreshToken} ***********");
            return true;
        }
        else
        {
            _logger.LogError($"Error: {_loginResult.ErrorDescription}");
            return false;
        }
    }

    [RelayCommand]
    public async Task LogoutAsync()
    {
        await _auth0Client.LogoutAsync();
        _loginResult = null;
        Tokens = null;
        UserProfile = null;
        SecureStorage.RemoveAll();
        _logger.LogInformation($"--------------------------------- 6. You are logged out! -----------------------");
    }

    public async Task<string> GetAccessTokenAsync()
    {
        Tokens = await GetCredentialsFromSecureStorage();
        UserProfile = await GetUserProfileFromSecureStorage();
        _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5. Access Token from SecureStorage: {Tokens!.AccessToken} +++++++++++++++++++");

        // Check if the access token is valid = it is not expired or there are 2 minutes or more left
        DateTimeOffset now = DateTime.UtcNow;
        DateTimeOffset exp = Tokens.AccessTokenExpiration!.Value.ToUniversalTime();

        if ((exp - now).TotalMinutes >= 2)
        {
            _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5a. Access Token is still valid in {(exp - now).TotalMinutes.ToString("F1")} minutter! +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            return Tokens.AccessToken!;
        }

        // We have an expired AccessToken, but luckily also a RefreshToken
        if (!string.IsNullOrEmpty(Tokens.RefreshToken))
        {
            _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5b. AccessToken is expired! RefreshToken exists: {Tokens.RefreshToken} => RefreshToken() +++++++++++++++++++");
            RefreshTokenResult refreshTokenResult = await _auth0Client.RefreshTokenAsync(Tokens.RefreshToken);
            if (!refreshTokenResult.IsError)
            {
                // Update the token in the TokenHolder
                await refreshTokenResult.ToTokens();
                _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5b. New AccessToken: {refreshTokenResult.AccessToken}  +++++++++++++++++++");
                _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5b. New AccessToken is valid to: {refreshTokenResult.AccessTokenExpiration} +++++++++++++++++++");
                _logger.LogInformation($"+++++++++++++++++++++++++++++++++ 5b. New AccessToken expires in: {refreshTokenResult.ExpiresIn} ++++++++++++++++++++++++++++++");
            }
        }
        else // RefreshToken is expired => we have to Authenticate!
        {
            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!! 5c. RefreshToken is expired - we call AuthenticateAsync() !!!!!!!!!!!!!!!!!!");
            await AuthenticateAsync();
        }
        return Tokens.AccessToken!;
    }

    private async Task<Tokens?> GetCredentialsFromSecureStorage()
    {
        var tokensJson = await SecureStorage.GetAsync(nameof(TokensSecureStorage));
        if (!string.IsNullOrEmpty(tokensJson))
        {
            try
            {
                return JsonSerializer.Deserialize<Tokens>(tokensJson);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!! REFRESH TOKEN ERROR: {ex} !!!!!!!!!!");
            }
        }
        return new Tokens();
    }

    public async Task<UserProfile?> GetUserProfileFromSecureStorage()
    {
        var userProfileJson = await SecureStorage.GetAsync(nameof(UserProfileSecureStorage));
        if (!string.IsNullOrEmpty(userProfileJson))
        {
            try
            {
                return JsonSerializer.Deserialize<UserProfile>(userProfileJson);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!  USER PROFILE ERROR: {ex} !!!!!!!!!!!");
            }
        }
        return new UserProfile();
    }

}
