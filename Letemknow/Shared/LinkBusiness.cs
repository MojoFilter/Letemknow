namespace Letemknow.Shared;

public interface ILinkBusiness
{
    string Escape(string? text);
    string GetLinkUri(string baseUri, MailLink link);
}

internal sealed class LinkBusiness : ILinkBusiness
{
    public string Escape(string? text) => Uri.EscapeDataString(text ?? string.Empty);

    public string GetLinkUri(string baseUri, MailLink link) => $"{baseUri}{link.Id}";

}
