using System.Net.Http.Json;

namespace Letemknow.Shared;

public interface ITrackingClient
{
    Task<IEnumerable<MailLink>?> GetAllLinksAsync(CancellationToken cancellationToken = default);
    Task TrackMailToClickAsync(string linkId, ClickTarget target, CancellationToken cancellationToken = default);
    Task<ClickStatData?> GetLinkStatsAsync(string linkId, CancellationToken cancellationToken = default);
}

public interface ILinkClient
{
    Task<MailLink?> GetLinkAsync(string? linkId, CancellationToken cancellationToken = default);
    Task<MailLink?> GenerateLink(CancellationToken cancellationToken = default);
    Task SubmitLinkAsync(MailLink link, CancellationToken cancellationToken = default);
}

internal sealed class ApiClient : ITrackingClient, ILinkClient
{
    public ApiClient(HttpClient client, IClientBusiness clientBusiness)
    {
        _client = client;
        _clientBusiness = clientBusiness;
    }

    public async Task TrackMailToClickAsync(string linkId, ClickTarget target, CancellationToken cancellationToken = default)
    {
        var data = new MailLinkClick()
        {
            LinkId = linkId,
            Target = target,
            Referrer = await _clientBusiness.GetReferrerAsync(),
            UserAgent = await _clientBusiness.GetUserAgentAsync()
        };
        await _client.PostAsJsonAsync("api/Track", data, cancellationToken);
    }

    public Task<IEnumerable<MailLink>?> GetAllLinksAsync(CancellationToken cancellationToken = default) =>
        _client.GetFromJsonAsync<IEnumerable<MailLink>>("api/Link/list", cancellationToken);

    public Task<MailLink?> GetLinkAsync(string? linkId, CancellationToken cancellationToken = default) =>
        _client.GetFromJsonAsync<MailLink>($"api/Link?id={linkId}", cancellationToken);

    public Task<ClickStatData?> GetLinkStatsAsync(string linkId, CancellationToken cancellationToken = default) =>
        _client.GetFromJsonAsync<ClickStatData>($"api/Track/{linkId}", cancellationToken);

    public Task<MailLink?> GenerateLink(CancellationToken cancellationToken = default) =>
        _client.GetFromJsonAsync<MailLink>("api/Link/gen", cancellationToken);

    public Task SubmitLinkAsync(MailLink link, CancellationToken cancellationToken = default) =>
        _client.PostAsJsonAsync("api/Link", link);

    private readonly HttpClient _client;
    private readonly IClientBusiness _clientBusiness;
}
