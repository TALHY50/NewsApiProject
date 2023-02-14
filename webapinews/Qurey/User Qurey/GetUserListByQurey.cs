using MediatR;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserListQuery() : IRequest<List<User>>;
}
