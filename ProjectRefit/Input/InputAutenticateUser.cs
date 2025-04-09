namespace ProjectRefit.Input;

public class InputAutenticateUser(string username, string password, int expiresInMins)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public int ExpiresInMins { get; set; } = expiresInMins;
}