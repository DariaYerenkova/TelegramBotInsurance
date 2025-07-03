using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IDocumentRepository Documents { get; }
        IExtractedFieldRepository ExtractedFields { get; }
        void Save();
        Task SaveAsync();
    }
}
