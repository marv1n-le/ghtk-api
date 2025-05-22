using Microsoft.AspNetCore.Authentication;

namespace Ghtk.Authorization;

public static class XClientSourceAuthenticationHandlerExtenstions
{
    public static AuthenticationBuilder AddXClientSource(
        this AuthenticationBuilder builder, Action<XClientSourceAuthenticationHandlerOptions> configure)
    {
        return builder.AddScheme<XClientSourceAuthenticationHandlerOptions, XClientSourceAuthenticationHandler>("X-Client-Source",
            configure);
    }
}