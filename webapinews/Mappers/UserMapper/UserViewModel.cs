using webapinews.Entities;

namespace webapinews.Mappers.UserMapper
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }
         public DateTime CreatedDate { get; set; } 
        public DateTime ModifiedDate { get; set; }
    }
}
