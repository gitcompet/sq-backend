namespace JwtWebApiDotNet7.Models
{
    public class User
    {   
        public User(string username,string password) { 
            this.Username = username;
            this.PasswordHash = password;
        }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
