using ProjectRefit.Interface.Service;
using System.Net.Http.Headers;

namespace ProjectRefit.Handler;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ITokenService _tokenService;

    public AuthTokenHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri != null && !request.RequestUri.AbsolutePath.Contains("/auth/login", StringComparison.OrdinalIgnoreCase))
            TryAuthenticate(request);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return response;

        TryAuthenticate(request);
        response = await base.SendAsync(request, cancellationToken);

        return response;
    }

    internal async void TryAuthenticate(HttpRequestMessage request)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}