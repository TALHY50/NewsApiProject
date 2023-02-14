using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;

namespace webapinews.Handler.BookMark_Handler
{
    public class GetBookMarkByIdHandler : IRequestHandler<GetBookMarkByIdQuery,List<BookMarksViewModel>>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;

        public GetBookMarkByIdHandler(IBookMarkReporistory bookMarkReporistory)
        {
            _bookMarkReporistory = bookMarkReporistory;
        }
        public Task <List<BookMarksViewModel>> Handle(GetBookMarkByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookMarkReporistory.GetById(request.id));
        }
    }
}
