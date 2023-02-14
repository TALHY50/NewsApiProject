using MediatR;
using webapinews.Entities;
using webapinews.Models;

namespace webapinews.Qurey.News_Qurey
{
    public record GetBookMarkByIdQuery(int id) : IRequest< List<BookMarksViewModel>>;
}
