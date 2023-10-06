namespace Letemknow.Shared;

public interface ILinkBusiness
{
    string Escape(string? text);
}

internal sealed class LinkBusiness : ILinkBusiness
{
    public string Escape(string? text) => Uri.EscapeDataString(text ?? string.Empty);
}
