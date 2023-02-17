using MediatR;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Command.BookMark_Commands
{
    public record SaveBookMarkCommand(int newsId, int userId) : IRequest<BookMarksViewModel>;
}
