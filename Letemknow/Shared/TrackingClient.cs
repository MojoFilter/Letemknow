using System.Net.Http.Json;

namespace Letemknow.Shared;

public interface ITrackingClient
{
    Task TrackMailToClick(ClickTarget target, CancellationToken cancellationToken = default);
}

internal sealed class TrackingClient : ITrackingClient 
{
    public TrackingClient(HttpClient client)
    {
        _client = client;
    }

    public Task TrackMailToClick(ClickTarget target, CancellationToken cancellationToken = default) => 
        _client.PostAsJsonAsync("Track", new MailLinkClick() { Target = target }, cancellationToken);

    private readonly HttpClient _client;
}
