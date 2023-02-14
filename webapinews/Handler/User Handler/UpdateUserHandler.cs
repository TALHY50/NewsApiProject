using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserReporistory _userReporistory;
        public UpdateUserHandler(IUserReporistory userReporistory)
        {
            _userReporistory = userReporistory;
        }
        public Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.Update(request.model));
        }
    }
}
