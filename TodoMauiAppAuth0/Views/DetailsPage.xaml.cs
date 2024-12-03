using TodoMauiAppAuth0.ViewModels;

namespace TodoMauiAppAuth0.Views;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(ItemDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}