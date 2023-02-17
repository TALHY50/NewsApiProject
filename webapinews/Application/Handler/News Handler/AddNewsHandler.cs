using AutoMapper;
using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;
using webapinews.Services;

namespace webapinews.Handler.News_Handler
{
    public class AddNewsHandler : IRequestHandler<AddNewsCommand, AddNewsViewModel>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;
        public AddNewsHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
        _newsReporistory = newsReporistory;
        _mapper = mapper;
        }
        public async Task<AddNewsViewModel> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            var news = _mapper.Map<News>(request);
            var result = await Task.FromResult(_newsReporistory.Add(news));
            var viewModel = _mapper.Map<AddNewsViewModel>(result);
            return viewModel;
        }
    }
}
