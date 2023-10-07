using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Letemknow.Shared;

public class MailLinkClick
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset Timestamp { get; set; }

    public ClickTarget Target { get; set; }
    public IPAddress? Ip { get; set; }

    public MailLinkClick Create(DateTimeOffset timestamp) => new() { Timestamp = timestamp };
}

public enum ClickTarget
{
    MailClient,
    Gmail,
    Outlook,
    Yahoo
}
