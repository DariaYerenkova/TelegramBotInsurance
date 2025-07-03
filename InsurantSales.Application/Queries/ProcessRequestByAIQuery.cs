using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Queries
{
    public record ProcessRequestByAIQuery(string userInput) : IRequest<string>;
}
