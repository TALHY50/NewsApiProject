using MediatR;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record DeleteNewsCommand(int id) : IRequest<string>;
    
}
