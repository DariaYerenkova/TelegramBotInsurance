using InsurantSales.Application.Commands;
using InsurantSales.Application.Interfaces;
using InsurantSales.Application.Queries;
using InsurantSales.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Handlers
{
    public class GetUserByTelegramIdHandler : IRequestHandler<ProcessGetUserByTelegramIdQuery, User>
    {
        private readonly IUserService _userService;

        public GetUserByTelegramIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(ProcessGetUserByTelegramIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByTelegramIdAsync(request.telegramId);
        }
    }
}
