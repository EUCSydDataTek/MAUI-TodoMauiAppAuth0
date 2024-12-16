namespace TodoMauiAppAuth0;
public static class Config
{
    public static string BaseAddress { get; set; } = "https://todoapiauth0.azurewebsites.net/";
    public static string TodoitemsEndpoint { get; set; } = "todoitems";

    // Auth0 Config
    public static string Domain { get; set; } = "eucsyd.eu.auth0.com";
    public static string ClientId { get; set; } = "UEsF5peOE4UX6ItrIx4b6MZxBc7GVS20";
    public static string RedirectUri { get; set; } = "myapp://callback/";
    public static string PostLogoutRedirectUri { get; set; } = "myapp://callback/";
    public static string Scope { get; set; } = "openid profile email offline_access";
    public static string Audience { get; set; } = "https://todowebapi";
}
