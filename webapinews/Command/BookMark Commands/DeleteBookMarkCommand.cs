using MediatR;
using webapinews.Models;

namespace webapinews.Command.BookMark_Commands
{
    public record DeleteBookMarkCommand(int Id, int UserId) : IRequest<BookMark>;
}
