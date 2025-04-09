using Microsoft.Extensions.Caching.Memory;
using ProjectRefit.Input;
using ProjectRefit.Interface.Refit;
using ProjectRefit.Interface.Service;
using Refit;

namespace ProjectRefit.Service;

public class TokenService : ITokenService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemoryCache _cache;

    public TokenService(IServiceProvider serviceProvider, IMemoryCache cache)
    {
        _serviceProvider = serviceProvider;
        _cache = cache;
    }

    public async Task<string> GetTokenAsync()
    {
        if (_cache.TryGetValue("userLogged", out dynamic? userLogged) && !userLogged?.Expired)
            return userLogged != null ? userLogged.Token : "";

        if (userLogged is null)
            throw new Exception("Usuario não logado.");

        var loginRequest = new InputAutenticateUser(userLogged?.Username, userLogged?.Password, 1);

        var harmonRefit = RestService.For<IUserRefit>("https://dummyjson.com");

        var response = await harmonRefit.Login(loginRequest);
        string token = response.AccessToken;

        var expiration = TimeSpan.FromSeconds(60);
        _cache.Set("userLogged", new { Username = userLogged?.Username, Password = userLogged?.Password, Token = response.AccessToken, Expired = false });

        return token;
    }
}