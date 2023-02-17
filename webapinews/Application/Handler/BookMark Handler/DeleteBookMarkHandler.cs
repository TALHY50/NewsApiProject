using AutoMapper;
using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Command.News_Commands;
using webapinews.Controllers;
using webapinews.Interface;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Reporistory;
using webapinews.Services;

namespace webapinews.Handler.BookMark_Handler
{
    public class DeleteBookMarkHandler : IRequestHandler<DeleteBookMarkCommand, string>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        private readonly IMapper _mapper;
        public DeleteBookMarkHandler(IBookMarkReporistory bookMarkReporistory ,IMapper mapper)
        {
            _bookMarkReporistory = bookMarkReporistory;
            _mapper = mapper;
        }
        public Task<string> Handle(DeleteBookMarkCommand request, CancellationToken cancellationToken)
        {
            
            var result = _bookMarkReporistory.Delete(request.id,request.userId);
            var viewModel = _mapper.Map<string>(result);
            return Task.FromResult(viewModel);
        }
    }
}
