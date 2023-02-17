using AutoMapper;
using MediatR;
using webapinews.Interface;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Services;

namespace webapinews.Handler.News_Handler
{
  
    public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, NewsViewModel>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;
        public GetNewsByIdHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
             _newsReporistory = newsReporistory;
            _mapper = mapper;
        }
        public async Task<NewsViewModel> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        { 
            var result = await Task.FromResult(_newsReporistory.GetById(request.Id));
            var viewModel = _mapper.Map<NewsViewModel>(result);
            return viewModel;
        }
    }
}
