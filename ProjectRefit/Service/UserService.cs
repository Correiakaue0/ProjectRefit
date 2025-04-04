using Microsoft.Extensions.Caching.Memory;
using ProjectRefit.Input;
using ProjectRefit.Interface.Refit;
using ProjectRefit.Interface.Service;
using ProjectRefit.Output;

namespace ProjectRefit.Service
{
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
            _cache.Set("userActive", new { Username = inputAutenticateUser.Username, Password = inputAutenticateUser.Password});
            var result = await _userRefit.Login(inputAutenticateUser);
            _cache.Set("userLogged", new { Username = inputAutenticateUser.Username, Password = inputAutenticateUser.Password, Token = result.AccessToken });

            return result;
        }

        public dynamic GetUser()
        {
            return _userRefit.Me();
        }
    }
}