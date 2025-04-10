using ProjectRefit.Input;
using ProjectRefit.Output;
using Refit;

namespace ProjectRefit.Interface.Refit;

public interface IUserRefit
{
    [Post("/auth/login")]
    Task<OutputAutenticateUser> Login([Body] InputAutenticateUser inputAutenticateUser);

    [Post("/auth/refresh")]
    Task<OutputRefreshToken> RefreshToken([Body] InputRefreshToken inputRefreshToken);

    [Get("/auth/me")]
    Task<dynamic> GetUser();
}