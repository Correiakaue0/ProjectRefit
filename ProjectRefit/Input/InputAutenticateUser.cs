namespace ProjectRefit.Input
{
    public class InputAutenticateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int ExpiresInMins { get; set; }

        public InputAutenticateUser(string username, string password, int expiresInMins)
        {
            Username = username;
            Password = password;
            ExpiresInMins = expiresInMins;
        }
    }
}
