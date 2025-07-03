using InsurantSales.Application.TelegramBotService.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using InsurantSales.Application.TelegramBotHandler.Interfaces;

namespace InsurantSales.Application.TelegramBotService
{
    public class BotService : IBotService
    {
        private readonly IConfiguration _config;
        private readonly IBotUpdateHandler _updateHandler;

        public BotService(IConfiguration config, IBotUpdateHandler updateHandler)
        {
            _config = config;
            _updateHandler = updateHandler;
        }

        public async Task StartAsync()
        {
            var botToken = _config["Telegram:BotToken"]!;
            Environment.SetEnvironmentVariable("TELEGRAM_BOT_TOKEN", botToken);
            var botClient = new TelegramBotClient(botToken);

            using var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
             updateHandler: HandleUpdateAsync,
             errorHandler: HandlePollingErrorAsync,
             receiverOptions: receiverOptions,
             cancellationToken: cancellationToken);


            var me = await botClient.GetMe();
            Console.WriteLine($"Bot started: {me.Username}");

            await Task.Delay(-1, cts.Token);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            await _updateHandler.HandleAsync(update);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
        {
            var error = exception switch
            {
                ApiRequestException apiEx => $"Telegram API Error:\n[{apiEx.ErrorCode}]\n{apiEx.Message}",
                _ => exception.ToString()
            };

            return Task.CompletedTask;
        }
    }
}
