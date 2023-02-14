using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.BookMark_Handler
{
   
    public class SaveBookMarkHandler : IRequestHandler<SaveBookMarkCommand, List<BookMark>>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        public SaveBookMarkHandler(IBookMarkReporistory bookMarkReporistory)
        {
            _bookMarkReporistory = bookMarkReporistory;
        }
        public Task <List<BookMark>> Handle(SaveBookMarkCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookMarkReporistory.BookMarkNews(request.newsId , request.userId));
        }
    }
}
