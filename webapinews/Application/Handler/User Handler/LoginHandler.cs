using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class LoginHandler : IRequestHandler<loginCommand, UserInputResponse>
    {
        private readonly IUserReporistory _userReporistory;
        public LoginHandler(IUserReporistory userReporistory)
        {
            _userReporistory =  userReporistory;
        }
        public  Task<UserInputResponse> Handle(loginCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.Authenticate(request.userDataRequest));
        }
    }
}
