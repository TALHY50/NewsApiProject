using AutoMapper;
using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class RegistorUserHandler : IRequestHandler<RegistorUserCommand, RegistorUserViewModel>
    {
        private readonly IUserReporistory _userReporistory;
        private readonly IMapper _mapper;

        public RegistorUserHandler(IUserReporistory userReporistory, IMapper mapper)
        {
            _userReporistory = userReporistory;
            _mapper = mapper;
        }

        public async  Task<RegistorUserViewModel> Handle(RegistorUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var result =_userReporistory.Register(user);
            var viewModel = _mapper.Map<RegistorUserViewModel>(result);
            return await Task.FromResult(viewModel);
        }
    }


}
