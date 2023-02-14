using MediatR;
using webapinews.Helpers;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserPagenatedQuery(OwnerStringParameter ownerStringParameter) : IRequest<PaginatedList<User>>;
}
