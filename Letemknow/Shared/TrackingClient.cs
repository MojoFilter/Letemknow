using System.Net;
using System.Net.Http.Json;

namespace Letemknow.Shared;

public interface ITrackingClient
{
    Task<IEnumerable<MailLink>?> GetAllLinksAsync(CancellationToken cancellationToken = default);
    Task TrackMailToClickAsync(string linkId, ClickTarget target, CancellationToken cancellationToken = default);
    Task<ClickStatData?> GetLinkStatsAsync(string linkId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DateClicks>> GetClicksByDateAsync(MailLink link, ClickTarget target, DateOnly startDate, DateOnly endDate, CancellationToken cancellation = default);
}

public interface ILinkClient
{
    Task<MailLink?> GetLinkAsync(string? linkId, CancellationToken cancellationToken = default);
    Task<MailLink?> GenerateLink(CancellationToken cancellationToken = default);
    Task SubmitLinkAsync(MailLink link, CancellationToken cancellationToken = default);
}

public record class DateClicks(DateOnly Date, int Clicks);


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

    public async Task<IEnumerable<DateClicks>> GetClicksByDateAsync(MailLink link, ClickTarget target, DateOnly startDate, DateOnly endDate, CancellationToken cancellation = default)
    {
        var uri = $"api/Track/{link.Id}/{target}/?startDate={startDate}&endDate={endDate}";
        return await _client.GetFromJsonAsync<IEnumerable<DateClicks>>(uri, cancellation) ?? Enumerable.Empty<DateClicks>();
    }

    private readonly HttpClient _client;
    private readonly IClientBusiness _clientBusiness;
}
