using TodoMauiAppAuth0.ViewModels;

namespace TodoMauiAppAuth0.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}