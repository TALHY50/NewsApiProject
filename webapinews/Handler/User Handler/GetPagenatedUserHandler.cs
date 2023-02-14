using MediatR;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{
    public class GetPagenatedUserHandler : IRequestHandler<GetUserPagenatedQuery, PaginatedList<User>>
    {
        private readonly IUserReporistory _userReporistory;
        public GetPagenatedUserHandler(IUserReporistory userReporistory)
        {
            _userReporistory = userReporistory;
        }

        public Task<PaginatedList<User>> Handle(GetUserPagenatedQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userReporistory.Get(request.ownerStringParameter));
        }
    }
}
