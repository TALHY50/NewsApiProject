using MediatR;
using webapinews.Entities;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Models;

namespace webapinews.Command.BookMark_Commands
{
    public record DeleteBookMarkCommand(int id, int userId) : IRequest<string>
    {
    }
    
}
