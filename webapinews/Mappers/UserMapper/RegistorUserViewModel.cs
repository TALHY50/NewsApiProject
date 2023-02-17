using System.Runtime.InteropServices;
using webapinews.Models;

namespace webapinews.Mappers
{
    public class RegistorUserViewModel
    { 
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    
}
