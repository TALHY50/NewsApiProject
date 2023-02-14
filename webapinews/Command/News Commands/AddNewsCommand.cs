using MediatR;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record AddNewsCommand(News news) : IRequest<News>;
}
