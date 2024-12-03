namespace TodoMauiAppAuth0;
public static class Config
{
    public static string BaseAddress { get; set; } = "https://?????-???.euw.devtunnels.ms";
    public static string TodoitemsEndpoint { get; set; } = "todoitems";

    // Auth0 Config
    public static string Domain { get; set; } = "<YOUR AUTH0 DOMAIN>";
    public static string ClientId { get; set; } = "CLIENTID FOR CLIENT APP";
    public static string RedirectUri { get; set; } = "myapp://callback/";
    public static string PostLogoutRedirectUri { get; set; } = "myapp://callback/";
    public static string Scope { get; set; } = "openid profile email offline_access";
    public static string Audience { get; set; } = "<AUDIENCE FOR YOUR WEBAPI APP>";
}
