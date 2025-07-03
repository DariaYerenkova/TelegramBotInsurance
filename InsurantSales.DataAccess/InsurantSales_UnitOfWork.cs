using InsurantSales.DataAccess.Interfaces;
using InsurantSales.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess
{
    public class InsurantSales_UnitOfWork : IUnitOfWork
    {
        private readonly InsurantSales_DataContext _insurantSales_DataContext;
        private IUserRepository userRepository;
        private IDocumentRepository documentRepository;
        private IExtractedFieldRepository extractedFieldRepository;

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_insurantSales_DataContext);
                return userRepository;
            }
        }

        public IDocumentRepository Documents
        {
            get
            {
                if (documentRepository == null)
                    documentRepository = new DocumentRepository(_insurantSales_DataContext);
                return documentRepository;
            }
        }

        public IExtractedFieldRepository ExtractedFields
        {
            get
            {
                if (extractedFieldRepository == null)
                    extractedFieldRepository = new ExtractedFieldRepository(_insurantSales_DataContext);
                return extractedFieldRepository;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _insurantSales_DataContext.SaveChangesAsync();
        }

        public InsurantSales_UnitOfWork(InsurantSales_DataContext insurantSales_DataContext)
        {
            _insurantSales_DataContext = insurantSales_DataContext;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _insurantSales_DataContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
