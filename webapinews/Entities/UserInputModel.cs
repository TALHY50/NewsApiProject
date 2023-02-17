using System.ComponentModel.DataAnnotations;

namespace webapinews.Entities
{
    public class UserInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
