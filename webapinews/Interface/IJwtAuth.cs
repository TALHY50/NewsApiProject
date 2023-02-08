using webapinews.Models;

namespace webapinews.Interface
{
    public interface IJwtAuth
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
    }
}
