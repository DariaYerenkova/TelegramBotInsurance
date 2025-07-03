using InsurantSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess.Interfaces
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        Task<User> GetByTelegramIdAsync(long id);
    }
}
