using InsurantSales.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Commands
{
    public record SaveDocumentToDBCommand(Document document) : IRequest<Guid>;
}
