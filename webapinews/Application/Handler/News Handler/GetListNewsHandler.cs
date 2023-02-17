using AutoMapper;
using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Services;

namespace webapinews.Handler.News_Handler
{
    public class GetListNewsHandler : IRequestHandler<GetNewsListQuery, List<NewsViewModel>>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;
        public GetListNewsHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
            _newsReporistory = newsReporistory;
            _mapper = mapper;
        }
        public async Task<List<NewsViewModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var news = _newsReporistory.GetAll();
            var result = _mapper.Map<List<NewsViewModel>>(news);
            return await Task.FromResult(result);
        }
    }
}
