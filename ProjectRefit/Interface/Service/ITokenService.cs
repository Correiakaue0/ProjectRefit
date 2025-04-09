namespace ProjectRefit.Interface.Service;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}