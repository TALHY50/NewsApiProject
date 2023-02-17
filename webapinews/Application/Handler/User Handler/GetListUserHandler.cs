using AutoMapper;
using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Interface;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{
    public class GetListUserHandler : IRequestHandler<GetUserListQuery, List<UserViewModel>>
    {
        private readonly IUserReporistory _userReporistory;
        private readonly IMapper _mapper;
        public GetListUserHandler(IUserReporistory userReporistory, IMapper mapper)
        {
            _userReporistory = userReporistory;
            _mapper = mapper;
        }
        public  Task<List<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var user = _userReporistory.GetAll();
            var result =  _mapper.Map<List<UserViewModel>>(user);
            return  Task.FromResult(result);

        }
    }
}
