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
    public class DeleteNewsHandler : IRequestHandler<DeleteNewsCommand, string>
    {
        private readonly INewsReporistory _newsReporistory;
        private readonly IMapper _mapper;
        public DeleteNewsHandler(INewsReporistory newsReporistory, IMapper mapper)
        {
            _newsReporistory = newsReporistory;
            _mapper=mapper;
        }
        public async Task<string> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(_newsReporistory.Delete(request.id));
            return result;
        }
    }
}
