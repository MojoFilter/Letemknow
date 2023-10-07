using System.ComponentModel.DataAnnotations;

namespace Letemknow.Shared;

public class MailLink
{
    [Key]
    public string? Id { get; set; }
    public string? Recipient { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? Created { get; set; }
}
