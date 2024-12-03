using CommunityToolkit.Mvvm.Messaging;
using TodoMauiAppAuth0.Messages;
using TodoMauiAppAuth0.Views;

namespace TodoMauiAppAuth0
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(NewEditItemPage), typeof(NewEditItemPage));
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Send a logout-message to the LoginViewModel
            WeakReferenceMessenger.Default.Send(new LogoutMessage("logout"));
        }
    }
}
