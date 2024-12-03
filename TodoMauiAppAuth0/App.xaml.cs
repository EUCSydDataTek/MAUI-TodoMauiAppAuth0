using System.Globalization;

namespace TodoMauiAppAuth0;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        CultureInfo cultureInfo = new("da-DK");
        Thread.CurrentThread.CurrentCulture = cultureInfo;
    }


    protected override Window CreateWindow(IActivationState activationState)
    {

        var shell = new AppShell();
        var window = new Window(shell);

        window.Created += (s, e) =>
        {
            Console.WriteLine("+++++++++++ CREATED ++++++++++++++");
        };

        window.Stopped += (s, e) =>
        {
            Console.WriteLine("++++++++++ STOPPED +++++++++++++++");
        };

        window.Resumed += (s, e) =>
        {
            Console.WriteLine("++++++++++ RESUMED +++++++++++++++");
            Shell.Current.GoToAsync("..");
            Windows[0].Page = new AppShell();
        };
        return window;
    }
}