using AutoMapper;
using MediatR;
using webapinews.Command.News_Commands;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.ExceptionHandler;
using webapinews.Interface;
using webapinews.Models;

namespace webapinews.Handler.User_Handler
{
    public class LoginHandler : IRequestHandler<loginCommand, UserInputResponse>
    {
        private readonly IUserReporistory _userReporistory;
        private IJwtAuth _jwtUtils;
        private readonly IMapper _mapper;
        public LoginHandler(IUserReporistory userReporistory, IJwtAuth jwtUtils, IMapper mapper)
        {
            _userReporistory =  userReporistory;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public  Task<UserInputResponse> Handle(loginCommand request, CancellationToken cancellationToken)
        {
            var user =  _userReporistory.Authenticate(request.userDataRequest);
            if (user == null)
                throw new AppException("Username or password is incorrect");

            var userResponse = _mapper.Map<UserInputResponse>(user);
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            userResponse.Token = jwtToken;
            return Task.FromResult(userResponse);
            
        }
    }
}
