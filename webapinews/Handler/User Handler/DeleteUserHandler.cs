using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly IUserReporistory _userReporistory;
        public DeleteUserHandler(IUserReporistory userReporistory)
        {
            _userReporistory = userReporistory;
        }
        public Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.Delete(request.Id));
        }
    }
}
