using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Command.News_Commands;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Reporistory;

namespace webapinews.Handler.BookMark_Handler
{
    public class DeleteBookMarkHandler : IRequestHandler<DeleteBookMarkCommand, BookMark>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        public DeleteBookMarkHandler(IBookMarkReporistory bookMarkReporistory)
        {
            _bookMarkReporistory = bookMarkReporistory;
        }
        public  Task<BookMark> Handle(DeleteBookMarkCommand request, CancellationToken cancellationToken)
        {
            return  Task.FromResult(_bookMarkReporistory.Delete(request.Id,request.UserId));
        }
    }
}
