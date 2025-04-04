using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Caching.Memory;
using ProjectRefit.Input;
using ProjectRefit.Interface.Refit;
using ProjectRefit.Interface.Service;
using Refit;

namespace ProjectRefit.Service
{
    public class TokenService : ITokenService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "auth_token";

        public TokenService(IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        public async Task<string> GetTokenAsync()
        {
            if (_cache.TryGetValue("userLogged", out dynamic userLogged))
                    return userLogged.Token; //verificar se o toke esta expirado

            _cache.TryGetValue("userActive", out dynamic userActive);
            var loginRequest = new InputAutenticateUser(userActive.Username, userActive.Password, 1);

            var harmonRefit = RestService.For<IUserRefit>("https://dummyjson.com");

            var response = await harmonRefit.Login(loginRequest);
            string token = response.AccessToken;

            var expiration = TimeSpan.FromSeconds(60);
            _cache.Set(CacheKey, token, expiration);

            return token;
        }
    }
}
