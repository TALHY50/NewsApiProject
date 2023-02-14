using MediatR;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Qurey.User_Qurey;

namespace webapinews.Handler.User_Handler
{

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IMediator _mediator;
        public GetUserByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _mediator.Send(new GetUserListQuery());
            var result = user.FirstOrDefault(u => u.Id == request.id);
            return result;
        }
    }
}
