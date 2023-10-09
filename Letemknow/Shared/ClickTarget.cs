namespace Letemknow.Shared;

public enum ClickTarget
{
    MailClient,
    Gmail,
    Outlook,
    Yahoo
}

public static class ClickTargetExtensions
{
    public static string Title(this ClickTarget target) => target switch
    {
        ClickTarget.MailClient => "Default Mail Client",        
        _ => target.ToString()
    };
}
