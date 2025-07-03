using InsurantSales.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Queries
{
    public record ProcessGetUserByTelegramIdQuery(long telegramId) : IRequest<User>;
}
