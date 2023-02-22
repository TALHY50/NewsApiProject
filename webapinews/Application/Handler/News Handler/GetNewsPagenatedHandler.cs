using AutoMapper;
using MediatR;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Interface;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Services;

namespace webapinews.Handler.News_Handler
{
  
   public class GetNewsPagenatedHandler : IRequestHandler<GetNewsPagenatedQuery, PaginatedList<NewsViewModel>>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;

        public GetNewsPagenatedHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
            _newsReporistory = newsReporistory;
            _mapper = mapper;
        }

        public async Task<PaginatedList<NewsViewModel>> Handle(GetNewsPagenatedQuery request, CancellationToken cancellationToken)
        {
            var result =  _newsReporistory.Get(request.paginatedViewModel);
            //var newsVMs = _mapper.Map<NewsViewModel>(result.Items);
            var viewModel = _mapper.Map<PaginatedList<NewsViewModel>>(result);
            return await Task.FromResult(viewModel);
        }
    }
}
