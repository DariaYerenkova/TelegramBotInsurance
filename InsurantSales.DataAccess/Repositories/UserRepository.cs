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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly InsurantSales_DataContext _dataContext;

        public UserRepository(InsurantSales_DataContext insurantSales_DataContext) : base(insurantSales_DataContext)
        {
            this._dataContext = insurantSales_DataContext;
        }

        public async Task<User> GetByTelegramIdAsync(long id)
        {
            return await _dataContext.Users.Include(u => u.Documents).FirstOrDefaultAsync(u => u.TelegramId == id);
        }
    }
}
