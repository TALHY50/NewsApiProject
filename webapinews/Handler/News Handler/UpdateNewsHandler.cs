using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.News_Handler
{

    public class UpdateNewsHandler : IRequestHandler<UpdateNewsCommand, News>
    {
        private readonly INewsReporistory _newsReporistory;
        public UpdateNewsHandler(INewsReporistory newsReporistory)
        {
            _newsReporistory = newsReporistory;
        }
        public Task<News> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_newsReporistory.Update(request.id, request.news)); 
        }
    }
}
