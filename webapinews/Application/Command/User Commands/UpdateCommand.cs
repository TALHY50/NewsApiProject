using MediatR;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record UpdateUserCommand : IRequest<UpdateUserViewModel>
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
