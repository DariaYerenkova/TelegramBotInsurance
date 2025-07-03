using InsurantSales.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
    {
        protected readonly InsurantSales_DataContext _dataContext;

        protected RepositoryBase(InsurantSales_DataContext insurantSales_DataContext)
        {
            _dataContext = insurantSales_DataContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dataContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dataContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Remove(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
        }
    }
}
