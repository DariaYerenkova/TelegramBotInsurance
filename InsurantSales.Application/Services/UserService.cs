using InsurantSales.Application.Interfaces;
using InsurantSales.DataAccess.Interfaces;
using InsurantSales.Domain.Entities;
using InsurantSales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _uow.Users.AddAsync(user);
            await _uow.SaveAsync();
        }

        public async Task UpdateUserDocumentStepAsync(Enums.DocumentStep step, long telegramId)
        {
            var user = await GetUserByTelegramIdAsync(telegramId);
            user.Step = step;
            await _uow.SaveAsync();
        }

        public async Task<User> GetUserByTelegramIdAsync(long id)
        {
            return await _uow.Users.GetByTelegramIdAsync(id);
        }

    }
}
