namespace ProjectRefit.Input;

public class InputRefreshToken(string refreshToken, int expiresInMins)
{
    public string RefreshToken { get; set; } = refreshToken;
    public int ExpiresInMins { get; set; } = expiresInMins;
}