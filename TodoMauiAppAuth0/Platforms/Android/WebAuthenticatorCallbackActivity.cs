using Android.App;
using Android.Content;
using Android.Content.PM;

namespace TodoMauiAppAuth0.Platforms.Android;
[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataScheme = CALLBACK_SCHEME)]
public class WebAuthenticatorActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "myapp";
}
