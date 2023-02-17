using AutoMapper;
using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserViewModel>
    {
        private readonly IUserReporistory _userReporistory;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUserReporistory userReporistory, IMapper mapper)
        {
            _userReporistory = userReporistory;
            _mapper = mapper;
        }
        public  Task<UpdateUserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            
            var user = _mapper.Map<User>(request);
            var result = _userReporistory.Update(user);
            var viewModel =  _mapper.Map<UpdateUserViewModel>(result);
            return Task.FromResult(viewModel);
        }
    }
}
