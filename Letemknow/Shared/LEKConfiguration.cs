using Letemknow.Shared;

namespace Microsoft.Extensions.DependencyInjection;

public static class LEKConfiguration
{
    public static IServiceCollection AddLEKClient(this IServiceCollection services) =>
        services.AddTransient<ILinkBusiness, LinkBusiness>();
}
