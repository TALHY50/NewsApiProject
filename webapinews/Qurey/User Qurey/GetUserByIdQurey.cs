using MediatR;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserByIdQuery(int id) : IRequest<User>;
}
