using MediatR;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;

namespace webapinews.Command.News_Commands
{
    public record UpdateNewsCommand : IRequest<UpdateNewsViewModel>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Aurthor { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }

}
