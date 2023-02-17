using MediatR;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record AddNewsCommand : IRequest<AddNewsViewModel>
    {
        public string Title { get; set; } = null!;

        public string Aurthor { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }
}
