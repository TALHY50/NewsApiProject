using MediatR;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsListQuery() : IRequest<List<News>>;
}
