using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Infrastructure.TelegramBotService.Interfaces
{
    public interface IBotService
    {
        Task StartAsync();
    }
}
