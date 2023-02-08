using System.ComponentModel.DataAnnotations;

namespace webapinews.Entities
{
    public class UserDataRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
