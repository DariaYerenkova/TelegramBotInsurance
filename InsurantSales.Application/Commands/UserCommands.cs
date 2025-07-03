using InsurantSales.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InsurantSales.Domain.Enums.Enums;

namespace InsurantSales.Application.Commands
{
    public record CreateUserCommand(User user) : IRequest;
    public record UpdateUserCommand(DocumentStep step, long telegramId) : IRequest;
}
