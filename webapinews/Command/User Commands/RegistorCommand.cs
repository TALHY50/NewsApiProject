using MediatR;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record RegistorUserCommand(User model) : IRequest<User>;
}
