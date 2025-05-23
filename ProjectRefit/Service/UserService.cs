using Microsoft.Extensions.Caching.Memory;
using ProjectRefit.Input;
using ProjectRefit.Interface.Refit;
using ProjectRefit.Interface.Service;
using ProjectRefit.Output;

namespace ProjectRefit.Service;

public class UserService : IUserService
{
    private readonly IUserRefit _userRefit;
    private readonly IMemoryCache _cache;

    public UserService(IUserRefit userRefit, IMemoryCache cache)
    {
        _userRefit = userRefit;
        _cache = cache;
    }

    public async Task<OutputAutenticateUser> Login(InputAutenticateUser inputAutenticateUser)
    {
        var result = await _userRefit.Login(inputAutenticateUser);
        _cache.Set("userLogged", new { Username = inputAutenticateUser.Username, Password = inputAutenticateUser.Password, Token = result.AccessToken, Expired = false });

        return result;
    }

    public async Task<dynamic> GetUser() => await _userRefit.GetUser();   
}