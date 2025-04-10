using ProjectRefit.Input;
using ProjectRefit.Output;

namespace ProjectRefit.Interface.Service;

public interface IUserService
{
    Task<OutputAutenticateUser> Login(InputAutenticateUser inputAutenticateUser);
    Task<dynamic> GetUser();
}