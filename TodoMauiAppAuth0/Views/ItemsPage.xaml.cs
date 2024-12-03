using TodoMauiAppAuth0.ViewModels;

namespace TodoMauiAppAuth0.Views;

public partial class ItemsPage : ContentPage
{
    readonly ItemsViewModel _vm;
    public ItemsPage(ItemsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetTodoItemsCommand.Execute(null);
    }
}