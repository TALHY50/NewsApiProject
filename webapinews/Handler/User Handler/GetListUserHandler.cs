using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{
    public class GetListUserHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly IUserReporistory _userReporistory;
        public GetListUserHandler(IUserReporistory userReporistory)
        {
            _userReporistory = userReporistory;
        }
        public Task <List<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.GetAll());
        }
    }
}
