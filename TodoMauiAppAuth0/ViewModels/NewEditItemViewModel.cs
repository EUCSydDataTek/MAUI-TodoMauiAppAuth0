using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using TodoMauiAppAuth0.Models;
using TodoMauiAppAuth0.Services.Data;

namespace TodoMauiAppAuth0.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class NewEditItemViewModel(ITodoItemService todoItemService, ILogger<NewEditItemViewModel> logger) : BaseViewModel
{
    int itemId;
    public string Id
    {
        get => Id;
        set => int.TryParse(value, out itemId);
    }

    [ObservableProperty]
    TodoItem? myTodoItem;

    [ObservableProperty]
    bool inValidDescription;

    [RelayCommand]
    public async Task OnAppearing()
    {
        MyTodoItem = itemId > 0
           ? await todoItemService.GetTodoItemAsync(itemId)
           : new TodoItem();
    }

    [RelayCommand]
    public async Task Delete()
    {
        if (MyTodoItem?.Id > 0)
        {
            HapticFeedback.Perform(HapticFeedbackType.Click);
            try
            {
                await todoItemService.DeleteItem(MyTodoItem.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"!!!!!!!!!! {nameof(NewEditItemViewModel)}: Unable to delete TodoItems: {ex.Message} !!!!!!!!!!");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task Save()
    {
        if (InValidDescription)
        {
            return;
        }
        HapticFeedback.Perform(HapticFeedbackType.Click);

        try
        {
            if (MyTodoItem!.Id == 0)
            {
                await todoItemService.CreateNewTodoItemAsync(MyTodoItem);
            }
            else
            {
                await todoItemService.EditTodoItemAsync(MyTodoItem);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"!!!!!!!!!! NewEditItemViewModel: Unable to add/edit TodoItem: {ex.Message} !!!!!!!!!!");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }

        await Shell.Current.GoToAsync("..");
    }
}
