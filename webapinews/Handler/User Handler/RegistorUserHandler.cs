using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class RegistorUserHandler : IRequestHandler<RegistorUserCommand, User>
    {
        private readonly IUserReporistory _userReporistory;
        public RegistorUserHandler(IUserReporistory userReporistory)
        {
            _userReporistory = userReporistory;
        }
        public Task<User> Handle(RegistorUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.Register(request.model));
        }
    }
}
