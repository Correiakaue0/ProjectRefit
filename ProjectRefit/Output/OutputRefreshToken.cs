namespace ProjectRefit.Output;

public class OutputRefreshToken(string accessToken, string refreshToken)
{
    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
}