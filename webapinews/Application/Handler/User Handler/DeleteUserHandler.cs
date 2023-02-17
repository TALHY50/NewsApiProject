using AutoMapper;
using MediatR;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Mappers.UserMapper;
using webapinews.Models;

//namespace webapinews.Handler.User_Handler
//{
//    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserViewModel>
//    {
//        private readonly IUserReporistory _userReporistory;
//        private readonly IMapper _mapper;
//        public DeleteUserHandler(IUserReporistory userReporistory, IMapper mapper)
//        {
//            _userReporistory = userReporistory;
//            _mapper = mapper;
//        }
//        public  Task<UserViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
//        {
//            var user = _mapper.Map<User>(request);
//            var result = _userReporistory.Delete(user.Id);
//            var viewModel =_mapper.Map<DeleteUserViewModel>(result);
//            return Task.FromResult(viewModel);
//        }
//    }
//}
