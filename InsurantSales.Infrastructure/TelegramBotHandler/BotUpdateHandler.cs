using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using InsurantSales.Infrastructure.TelegramBotHandler.Interfaces;
using InsurantSales.Infrastructure.OpenAIService.Interfaces;
using MediatR;
using InsurantSales.Application.Queries;

namespace InsurantSales.Infrastructure.TelegramBotHandler
{
    public class BotUpdateHandler : IBotUpdateHandler
    {        
        private readonly IMediator _mediator;

        public BotUpdateHandler(IOpenAIService ai, IMediator mediator)
        {
            _ai = ai;
            _mediator = mediator;
        }

        public async Task HandleAsync(Update update)
        {
            if (update.Message is { Text: { } messageText })
            {
                var chatId = update.Message.Chat.Id;

                string reply;

                if (messageText.ToLower().Contains("/start"))
                {
                    reply = await _mediator.Send(new ProcessStartQuery());
                }
                else
                {
                    reply = await _ai.GetAIReplyAsync(messageText);
                }

                var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")!);
                await botClient.SendMessage(chatId, reply);
            }
        }
    }
}
