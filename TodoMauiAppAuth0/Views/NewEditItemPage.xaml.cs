using TodoMauiAppAuth0.ViewModels;

namespace TodoMauiAppAuth0.Views;

public partial class NewEditItemPage : ContentPage
{
    private readonly NewEditItemViewModel vm;

    public NewEditItemPage(NewEditItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.AppearingCommand.ExecuteAsync(null);
    }
}