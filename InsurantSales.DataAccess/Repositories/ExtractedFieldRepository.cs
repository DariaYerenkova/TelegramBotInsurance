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
    public class ExtractedFieldRepository : RepositoryBase<ExtractedField>, IExtractedFieldRepository
    {
        private readonly InsurantSales_DataContext _dataContext;

        public ExtractedFieldRepository(InsurantSales_DataContext insurantSales_DataContext) : base(insurantSales_DataContext)
        {
            this._dataContext = insurantSales_DataContext;
        }        

        public async Task<ExtractedField> GetAsync(Guid id)
        {
            return await _dataContext.ExtractedFields.Include(f => f.Document).FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
