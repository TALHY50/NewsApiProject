using MediatR;
using webapinews.Helpers;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Qurey.User_Qurey
{
    public record GetUserPagenatedQuery(PaginatedViewModel paginatedViewModel) : IRequest<PaginatedList<UserViewModel>>;
}
