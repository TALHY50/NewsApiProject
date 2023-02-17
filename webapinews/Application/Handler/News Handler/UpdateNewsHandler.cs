using AutoMapper;
using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Services;

namespace webapinews.Handler.News_Handler
{

    public class UpdateNewsHandler : IRequestHandler<UpdateNewsCommand, UpdateNewsViewModel>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;
        public UpdateNewsHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
            _newsReporistory = newsReporistory;
            _mapper = mapper;
        }
        public async Task<UpdateNewsViewModel> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<News>(request);
            var result = _newsReporistory.Update(user);
            var viewModel = _mapper.Map<UpdateNewsViewModel>(result);
            return await Task.FromResult(viewModel);
        }
    }
}
