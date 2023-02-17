using MediatR;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record DeleteUserCommand(int Id) : IRequest<User>;
}
