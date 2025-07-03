using InsurantSales.Application.Commands;
using InsurantSales.Application.Interfaces;
using InsurantSales.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Handlers
{
    public class UserHandler : IRequestHandler<CreateUserCommand>, IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;

        public UserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(request.user);
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserDocumentStepAsync(request.step, request.telegramId);
        }
    }
}
