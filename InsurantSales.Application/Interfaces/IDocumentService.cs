using InsurantSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<string> SaveDocumentToLocalFolderAsync(Stream fileStream, string fileName, string telegramUserId);
        Task<Guid> SaveDocumentToDBAsync(Document document);
    }
}
