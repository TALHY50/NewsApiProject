using MediatR;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record UpdateUserCommand(User model) : IRequest<User>;
}
