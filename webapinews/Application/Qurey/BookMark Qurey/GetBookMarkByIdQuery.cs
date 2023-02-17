using MediatR;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetBookMarkByIdQuery(int id) : IRequest< List<BookMarksViewModel>>;
}
