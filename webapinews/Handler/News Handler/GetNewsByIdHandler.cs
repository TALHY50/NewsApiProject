using MediatR;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;

namespace webapinews.Handler.News_Handler
{
  
    public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, News>
    {
        private readonly IMediator _mediator;
        public GetNewsByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<News> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {

            var news = await _mediator.Send(new GetNewsListQuery());
            var result = news.FirstOrDefault(u => u.Id == request.id);
            return result;
        }
    }
}
