using MediatR;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record DeleteUserCommand(int Id) : IRequest<User>;
}
