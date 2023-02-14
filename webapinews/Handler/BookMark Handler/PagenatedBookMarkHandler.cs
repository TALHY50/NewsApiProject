using MediatR;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.BookMark_Qurey;
using webapinews.Qurey.News_Qurey;

namespace webapinews.Handler.BookMark_Handler
{
    public class PagenatedBookMarkHandler : IRequestHandler<PagenatedBookMarkNewsQurey, PaginatedList<BookMarksViewModel>>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        public PagenatedBookMarkHandler(IBookMarkReporistory bookMarkReporistory)
        {
            _bookMarkReporistory = bookMarkReporistory;
        }

        public Task<PaginatedList<BookMarksViewModel>> Handle(PagenatedBookMarkNewsQurey request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookMarkReporistory.GetBookMarkedById(request.id ,request.ownerStringParameter));
        }
    }
}
