using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InsurantSales.Application.TelegramBotHandler.Interfaces
{
    public interface IBotUpdateHandler
    {
        Task HandleAsync(Update updatea, CancellationToken cancellationToken = default);
    }
}
