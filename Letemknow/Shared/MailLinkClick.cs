using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Letemknow.Shared;

public class MailLinkClick
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset Timestamp { get; set; }
    public required string LinkId { get; set; }
    public ClickTarget Target { get; set; }
    public IPAddress? Ip { get; set; }
    public string? UserAgent {get;set;}
    public string? Referrer { get; set; }

    //public MailLinkClick Create(DateTimeOffset timestamp) => new() { Timestamp = timestamp };
}
