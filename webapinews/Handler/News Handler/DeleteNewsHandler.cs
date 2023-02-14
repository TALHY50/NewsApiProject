using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.News_Handler
{
    public class DeleteNewsHandler : IRequestHandler<DeleteNewsCommand, News>
    {
        private readonly INewsReporistory _newsReporistory;
        public DeleteNewsHandler(INewsReporistory newsReporistory)
        {
            _newsReporistory = newsReporistory;
        }
        public Task<News> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_newsReporistory.Delete(request.Id));
        }
    }
}
