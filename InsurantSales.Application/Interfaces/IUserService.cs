using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsurantSales.Domain.Entities;
using InsurantSales.Domain.Enums;

namespace InsurantSales.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task UpdateUserDocumentStepAsync(Enums.DocumentStep step, long telegramId);
        Task<User> GetUserByTelegramIdAsync(long id);
    }
}
