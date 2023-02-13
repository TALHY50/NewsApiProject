using webapinews.Models;

namespace webapinews.Entities
{
    public class UserDataResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public Role Role { get; set; }
        public string Token { get; set; }

        public UserDataResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Email = user.Email;
            Password = user.Password;
            //Role = user.Role;
            Token = token;
        }
    }
}
