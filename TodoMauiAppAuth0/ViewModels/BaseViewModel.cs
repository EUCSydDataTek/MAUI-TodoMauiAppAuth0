using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoMauiAppAuth0.ViewModels;
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string? title;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    bool isSpinnerRunning;
}
