using AutoMapper;
using MediatR;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{
    public class GetPagenatedUserHandler : IRequestHandler<GetUserPagenatedQuery, PaginatedList<UserViewModel>>
    {
        private readonly IUserReporistory _userReporistory;
        private readonly IMapper _mapper;
        public GetPagenatedUserHandler(IUserReporistory userReporistory, IMapper mapper)
        {
            _userReporistory = userReporistory;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserViewModel>> Handle(GetUserPagenatedQuery request, CancellationToken cancellationToken)
        {
            var result = _userReporistory.Get(request.paginatedViewModel);
            var viewModel = _mapper.Map<PaginatedList<UserViewModel>>(result);
            return await Task.FromResult(viewModel);
        }
    }
}
