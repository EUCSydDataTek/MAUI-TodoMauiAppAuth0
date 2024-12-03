using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using TodoMauiAppAuth0.Models;
using TodoMauiAppAuth0.Services.Data;
using TodoMauiAppAuth0.Views;

namespace TodoMauiAppAuth0.ViewModels;
public partial class ItemsViewModel(ITodoItemService todoItemService, IConnectivity connectivity, ILogger<ItemsViewModel> logger) : BaseViewModel
{
    public TodoItem? SelectedItem { get; set; }
    public ObservableCollection<TodoItem> TodoItems { get; } = new();

    #region COMMANDS

    [RelayCommand]
    public async Task GetTodoItems()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }
            logger.LogInformation("********************************* 4. GetCompletedTodoItemsAsync() **********************");

            IsBusy = true;
            IsSpinnerRunning = !IsRefreshing;
            IEnumerable<TodoItem> todoItems = await todoItemService.GetTodoItemsAsync();

            if (TodoItems.Count != 0)
                TodoItems.Clear();

            if (todoItems.Any())
            {
                foreach (var todoItem in todoItems)
                    TodoItems.Add(todoItem);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"!!!!!!!!!! ItemsViewModel: Unable to get TodoItems: {ex.Message} !!!!!!!!!!");
            await Shell.Current.DisplayAlert("Error in ItemsViewModel", "Unable to get TodoItems", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
            IsSpinnerRunning = false;
        }
    }

    [RelayCommand]
    async Task AddItem()
    {
        await Shell.Current.GoToAsync(nameof(NewEditItemPage), new Dictionary<string, object>
        {
            {"item", new TodoItem() { Description = "" } }
        });
    }

    [RelayCommand]
    async Task GoToDetails(TodoItem todoItem)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
        {
            {"item", todoItem }
        });
    }

    [RelayCommand]
    async Task SoftDelete(TodoItem todoItem)
    {
        HapticFeedback.Perform(HapticFeedbackType.Click);
        todoItem.Completed = true;
        try
        {
            await todoItemService.MarkItemCompleted(todoItem);
        }
        catch (Exception ex)
        {
            logger.LogError($"!!!!!!!!!! ItemsViewModel: Unable to softdelete TodoItem: {ex.Message} !!!!!!!!!!");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        await GetTodoItems();
    }

    [RelayCommand]
    async Task Edit(TodoItem todoItem)
    {
        HapticFeedback.Perform(HapticFeedbackType.Click);
        //await Shell.Current.GoToAsync(nameof(NewEditItemPage), new Dictionary<string, object>
        //{
        //    {"item", todoItem }
        //});

        await Shell.Current.GoToAsync($"{nameof(NewEditItemPage)}?id={todoItem.Id}");
    }
    #endregion
}
