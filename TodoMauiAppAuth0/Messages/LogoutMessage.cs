using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TodoMauiAppAuth0.Messages;
public class LogoutMessage : ValueChangedMessage<string>
{
    public LogoutMessage(string message) : base(message)
    {        
    }
}
