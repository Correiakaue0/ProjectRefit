namespace ProjectRefit.Output;

public class OutputAutenticateUser(int id, string username, string accessToken)
{
    public int Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string AccessToken { get; set; } = accessToken;
}