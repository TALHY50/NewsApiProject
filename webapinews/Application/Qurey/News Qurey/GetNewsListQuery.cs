using MediatR;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsListQuery() : IRequest<List<NewsViewModel>>;
}
