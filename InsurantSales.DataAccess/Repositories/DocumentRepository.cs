using InsurantSales.DataAccess.Interfaces;
using InsurantSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess.Repositories
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        private readonly InsurantSales_DataContext _dataContext;

        public DocumentRepository(InsurantSales_DataContext insurantSales_DataContext) : base(insurantSales_DataContext)
        {
            this._dataContext = insurantSales_DataContext;
        }

        public async Task<Document> GetAsync(Guid id)
        {
            return await _dataContext.Documents.Include(d => d.ExtractedFields).FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
