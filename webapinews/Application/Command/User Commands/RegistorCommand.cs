using MediatR;
using webapinews.Mappers;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record RegistorUserCommand : IRequest<RegistorUserViewModel> {

        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    };

}
