using MediatR;
using webapinews.Helpers;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetNewsPagenatedQuery(PaginatedViewModel paginatedViewModel) : IRequest<PaginatedList<NewsViewModel>>;
}
