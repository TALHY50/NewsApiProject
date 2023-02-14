using MediatR;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record UpdateNewsCommand(int id, News news) : IRequest<News>;
}
