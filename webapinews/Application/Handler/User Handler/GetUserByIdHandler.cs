using AutoMapper;
using MediatR;
using webapinews.Interface;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserReporistory _userReporistory;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IUserReporistory userReporistory, IMapper mapper)
        {
            _userReporistory = userReporistory;
           _mapper= mapper;
        }
        public  Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _userReporistory.GetById(request.id);
            var viewModel = _mapper.Map<UserViewModel>(result);
            return Task.FromResult(viewModel);
           
        }
    }
}
