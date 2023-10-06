namespace Letemknow.Shared;

public interface ITrackingClient { }

internal sealed class TrackingClient : ITrackingClient 
{
    public async Task TrackMailToClick(CancellationToken cancellationToken)
    {
        await _client.PostAsync("Track", default);
    }

    private readonly HttpClient _client;    
}
