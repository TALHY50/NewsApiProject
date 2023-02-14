using MediatR;
using webapinews.Helpers;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsPagenatedQuery(OwnerStringParameter ownerStringParameter) : IRequest<PaginatedList<News>>;
}
