using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.News_Handler
{
    public class AddNewsHandler : IRequestHandler<AddNewsCommand, News>
    {
        private readonly INewsReporistory _newsReporistory;
        public AddNewsHandler(INewsReporistory newsReporistory)
        {
        _newsReporistory = newsReporistory;
        }
        public Task<News> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_newsReporistory.Add(request.news));
        }
    }
}
