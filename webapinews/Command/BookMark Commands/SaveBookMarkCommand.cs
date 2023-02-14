using MediatR;
using webapinews.Models;

namespace webapinews.Command.BookMark_Commands
{
    public record SaveBookMarkCommand(int newsId, int userId) : IRequest<List<BookMark>>;
}
