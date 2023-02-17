using AutoMapper;
using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Models;
using webapinews.Reporistory;
using webapinews.Services;

namespace webapinews.Handler.BookMark_Handler
{

    public class SaveBookMarkHandler : IRequestHandler<SaveBookMarkCommand,  BookMarksViewModel>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        private readonly IMapper _mapper;
        public SaveBookMarkHandler(IBookMarkReporistory bookMarkReporistory, IMapper mapper)
        {
            _bookMarkReporistory = bookMarkReporistory;
            _mapper = mapper;
        }
        public async Task<BookMarksViewModel> Handle(SaveBookMarkCommand request, CancellationToken cancellationToken)
        {
            var bookMarks =  _bookMarkReporistory.BookMarkNews(request.newsId, request.userId);
            var viewModel =  _mapper.Map<BookMarksViewModel>(bookMarks);
            return viewModel;
        }


    }
}
