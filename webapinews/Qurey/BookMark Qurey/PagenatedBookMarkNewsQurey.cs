using MediatR;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;

namespace webapinews.Qurey.BookMark_Qurey
{

    public record PagenatedBookMarkNewsQurey(int id, OwnerStringParameter ownerStringParameter) : IRequest<PaginatedList<BookMarksViewModel>>;
}
