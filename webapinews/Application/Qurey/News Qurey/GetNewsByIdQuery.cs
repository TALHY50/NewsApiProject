using MediatR;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsByIdQuery(int Id) : IRequest<NewsViewModel>
    {
        
    }
}
