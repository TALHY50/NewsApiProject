using MediatR;
using webapinews.Helpers;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Qurey.BookMark_Qurey
{

    public record PagenatedBookMarkNewsQurey(int id, PaginatedViewModel paginatedViewModel) : IRequest<PaginatedList<BookMarksViewModel>>;
}
