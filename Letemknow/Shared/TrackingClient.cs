using System.Net.Http.Json;

namespace Letemknow.Shared;

public interface ITrackingClient
{
    Task<IEnumerable<MailLink>?> GetAllLinksAsync(CancellationToken cancellationToken = default);
    Task TrackMailToClickAsync(ClickTarget target, CancellationToken cancellationToken = default);
}

internal sealed class TrackingClient : ITrackingClient 
{
    public TrackingClient(HttpClient client)
    {
        _client = client;
    }

    public Task TrackMailToClickAsync(ClickTarget target, CancellationToken cancellationToken = default) => 
        _client.PostAsJsonAsync("Track", new MailLinkClick() { Target = target }, cancellationToken);

    public Task<IEnumerable<MailLink>?> GetAllLinksAsync(CancellationToken cancellationToken = default) =>
        _client.GetFromJsonAsync<IEnumerable<MailLink>>("Link/list", cancellationToken);
    

    private readonly HttpClient _client;
}
