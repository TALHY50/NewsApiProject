using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Services
{

    public class IdentityServices : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public int? GetUserId()
        {
            return GetUser()?.Id;
        }

        private User? GetUser()
        {
            return (User?)_httpContextAccessor.HttpContext?.Items.FirstOrDefault(item => item.Key.Equals("User")).Value;
        }
    }




}
