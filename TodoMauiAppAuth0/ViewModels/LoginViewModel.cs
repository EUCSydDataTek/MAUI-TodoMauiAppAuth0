using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TodoMauiAppAuth0.Messages;
using TodoMauiAppAuth0.Services.AuthServices;
using TodoMauiAppAuth0.Views;

namespace TodoMauiAppAuth0.ViewModels;
public partial class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;
    private readonly ILogger<LoginViewModel> _logger;

    public LoginViewModel(AuthService authService, ILogger<LoginViewModel> logger)
    {
        _authService = authService;
        _logger = logger;

        // Register for LogoutMessage
        WeakReferenceMessenger.Default.Register<LogoutMessage>(this, async (r, m) =>
        {
            if (m.Value == "logout")
            {
                await _authService.LogoutAsync();
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        });

        // Check for existing UserProfile in SecureStorage. If found, then go direct to ShowItemsPage
        if (authService.IsUserProfileCreated)
        {
            _logger.LogInformation("************************** 2. UserProfile exits and we navigate to ItemsPage(ViewModel) ***************************");
            Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        _logger.LogInformation("************************** 1. No UserProfile exits and we navigate to AuthService.AuthenticateAsync() ***************************");
        IsBusy = true;
        await _authService.AuthenticateAsync();
        IsBusy = false;
        await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
    }
}
