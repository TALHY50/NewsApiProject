using AutoMapper;
using MediatR;
using webapinews.Command.BookMark_Commands;
using webapinews.Interface;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Mappers.News_Mapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Reporistory;

namespace webapinews.Handler.BookMark_Handler
{
    public class GetBookMarkByIdHandler : IRequestHandler<GetBookMarkByIdQuery,List<BookMarksViewModel>>
    {
        private readonly IBookMarkReporistory _bookMarkReporistory;
        private readonly IMapper _mapper;

        public GetBookMarkByIdHandler(IBookMarkReporistory bookMarkReporistory, IMapper mapper)
        {
            _bookMarkReporistory = bookMarkReporistory;
            
        }
        public Task <List<BookMarksViewModel>> Handle(GetBookMarkByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookMarkReporistory.GetById(request.id));
        }
    }
}
