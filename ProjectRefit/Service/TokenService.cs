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
        _cache.TryGetValue("userLogged", out dynamic? userLogged);

        if (userLogged != null && !userLogged?.Expired)
            return userLogged != null ? userLogged.Token : "";

        if (userLogged is null)
            return "";

        var harmonRefit = RestService.For<IUserRefit>("https://dummyjson.com");

        var inputRefreshToken = new InputRefreshToken(userLogged?.Token ?? "", 1);
        var response = await harmonRefit.RefreshToken(inputRefreshToken);

        _cache.Set("userLogged", new { Username = userLogged?.Username, Password = userLogged?.Password, Token = response.RefreshToken, Expired = false });

        return response.RefreshToken;
    }
}