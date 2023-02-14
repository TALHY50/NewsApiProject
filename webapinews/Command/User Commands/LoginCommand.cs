﻿using MediatR;
using webapinews.Entities;
using webapinews.Models;

namespace webapinews.Command.User_Commands
{
    public record loginCommand(UserDataRequest userDataRequest) : IRequest<UserDataResponse>;
}
