using MediatR;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserListQuery() : IRequest<List<UserViewModel>>;
}
