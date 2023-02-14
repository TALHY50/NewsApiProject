using MediatR;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record DeleteNewsCommand(int Id) : IRequest<News>;
}
