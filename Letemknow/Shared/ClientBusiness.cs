using Microsoft.JSInterop;

namespace Letemknow.Shared;

public interface IClientBusiness
{
    ValueTask<string> GetReferrerAsync();
    ValueTask<string> GetUserAgentAsync();
}

internal sealed class ClientBusiness : IClientBusiness
{
    public ClientBusiness(IJSRuntime js)
    {
        _js = js;
    }

    public ValueTask<string> GetReferrerAsync() =>
        _js.InvokeAsync<string>("getReferrer");

    public ValueTask<string> GetUserAgentAsync() =>
        _js.InvokeAsync<string>("getUserAgent");

    private readonly IJSRuntime _js;
}


