using MediatR;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserByIdQuery(int id) : IRequest<UserViewModel>;
}
