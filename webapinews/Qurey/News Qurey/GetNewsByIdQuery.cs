using MediatR;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsByIdQuery(int id) : IRequest<News>;
}
