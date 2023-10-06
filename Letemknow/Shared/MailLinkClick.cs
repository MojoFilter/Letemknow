using System.ComponentModel.DataAnnotations.Schema;

namespace Letemknow.Shared;

public class MailLinkClick
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset Timestamp { get; set; }

    public MailLinkClick Create(DateTimeOffset timestamp) => new() { Timestamp = timestamp };
}
