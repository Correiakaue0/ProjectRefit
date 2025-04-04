using ProjectRefit.Interface.Service;
using System.Net.Http.Headers;

namespace ProjectRefit.Handler
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;

        public AuthTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.Contains("/auth/login", StringComparison.OrdinalIgnoreCase))
            {
                var token = await _tokenService.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Usuario deslogado ou Token expirado.");

            return response;
        }
    }
}
