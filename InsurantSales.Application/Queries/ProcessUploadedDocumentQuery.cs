using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InsurantSales.Application.Queries
{
    public record ProcessUploadedDocumentQuery(string filePaths, string documentId) : IRequest<string>;
    public record SaveDocumentToLocalFolderCommand(Stream fileStream, string fileName, string telegramUserId) : IRequest<string>;
}
