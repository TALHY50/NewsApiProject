using MediatR;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;

namespace webapinews.Handler.News_Handler
{
  
   public class GetNewsPagenatedHandler : IRequestHandler<GetNewsPagenatedQuery, PaginatedList<News>>
    {
        private readonly INewsReporistory _newsReporistory;
        public GetNewsPagenatedHandler(INewsReporistory newsReporistory)
        {
            _newsReporistory = newsReporistory;
        }

        public Task<PaginatedList<News>> Handle(GetNewsPagenatedQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_newsReporistory.Get(request.ownerStringParameter));
        }
    }
}
