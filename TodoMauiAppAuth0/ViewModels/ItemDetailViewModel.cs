using CommunityToolkit.Mvvm.ComponentModel;
using TodoMauiAppAuth0.Models;

namespace TodoMauiAppAuth0.ViewModels;

// https://docs.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation#process-navigation-data-using-query-property-attributes

[QueryProperty(nameof(TodoItem), "item")]
public partial class ItemDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    TodoItem? todoItem;
}
