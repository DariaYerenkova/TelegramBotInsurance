using Azure.Core;
using InsurantSales.Application.Interfaces;
using InsurantSales.DataAccess.Interfaces;
using InsurantSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InsurantSales.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _uow;

        public DocumentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> SaveDocumentToDBAsync(Document document)
        {
            document.Id = Guid.NewGuid();
            await _uow.Documents.AddAsync(document);
            await _uow.SaveAsync();

            return document.Id;
        }

        public async Task<string> SaveDocumentToLocalFolderAsync(Stream fileStream, string fileName, string telegramUserId)
        {
            var folder = Path.Combine("UploadedDocuments", telegramUserId);
            Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, fileName);

            fileStream.Position = 0;
            using var output = File.Create(fullPath);
            await fileStream.CopyToAsync(output);

            return fullPath;
        }
    }
}
