using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;

namespace webapinews.Handler.News_Handler
{
    public class GetListNewsHandler : IRequestHandler<GetNewsListQuery, List<News>>
    {
        private readonly INewsReporistory _newsReporistory;
        public GetListNewsHandler(INewsReporistory newsReporistory)
        {
            _newsReporistory = newsReporistory;
        }
        public Task<List<News>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_newsReporistory.GetAll());
        }
    }
}
