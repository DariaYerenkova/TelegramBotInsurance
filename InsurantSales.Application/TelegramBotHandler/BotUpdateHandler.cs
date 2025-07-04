using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using InsurantSales.Application.TelegramBotHandler.Interfaces;
using InsurantSales.Application.OpenAIService.Interfaces;
using MediatR;
using InsurantSales.Application.Queries;
using System.Reflection.Metadata;
using System.Threading;
using InsurantSales.Domain.Entities;
using static InsurantSales.Domain.Enums.Enums;
using InsurantSales.Application.Commands;
using Telegram.Bot.Types.ReplyMarkups;
using Mindee.Product.Fr.CarteGrise;

namespace InsurantSales.Application.TelegramBotHandler
{
    public class BotUpdateHandler : IBotUpdateHandler
    {
        private readonly IMediator _mediator;
        private readonly ITelegramBotClient _botClient;

        public BotUpdateHandler(IMediator mediator, ITelegramBotClient botClient)
        {
            _mediator = mediator;
            _botClient = botClient;
        }

        public async Task HandleAsync(Update update, CancellationToken cancellationToken = default)
        {
            if (update.Message is null)
                return;

            var chatId = update.Message.Chat.Id;
            string reply;
            Domain.Entities.User user = await _mediator.Send(new ProcessGetUserByTelegramIdQuery(update.Message.From.Id), cancellationToken);

            try
            {
                if (update.Message.Text == "/start")
                {
                    if (user == null)
                    {
                        var newUser = new Domain.Entities.User
                        {
                            TelegramId = update.Message.From?.Id,
                            Username = update.Message.From?.Username,
                            FirstName = update.Message.From?.FirstName,
                            LastName = update.Message.From?.LastName,
                            Step = DocumentStep.WaitingForCarDocument
                        };

                        await _mediator.Send(new CreateUserCommand(newUser), cancellationToken);
                    }
                    else
                    {
                        await _mediator.Send(new UpdateUserCommand(DocumentStep.WaitingForCarDocument, update.Message.From.Id));
                    }

                    reply = await _mediator.Send(new ProcessStartQuery(), cancellationToken);
                    await _botClient.SendMessage(chatId, reply, cancellationToken: cancellationToken);
                }
                else if (update.Message.Document is not null)
                {
                    var file = await _botClient.GetFile(update.Message.Document.FileId, cancellationToken);

                    var stream = new MemoryStream();
                    await _botClient.DownloadFile(file.FilePath!, stream, cancellationToken);
                    stream.Position = 0;

                    var filePaths = await _mediator.Send(new SaveDocumentToLocalFolderCommand(stream, update.Message.Document.FileName, update.Message.From.Id.ToString()), cancellationToken);
                    var document = new Domain.Entities.Document
                    {
                        DocumentFilePaths = filePaths,
                        DocumentName = user.Step == DocumentStep.WaitingForCarDocument ? "CarDocument" : "Passport",
                        UserId = user.Id,
                    };
                    var documentId = await _mediator.Send(new SaveDocumentToDBCommand(document), cancellationToken);

                    reply = await _mediator.Send(new ProcessUploadedDocumentQuery(filePaths, documentId.ToString()), cancellationToken);

                    await _botClient.SendMessage(chatId, reply, cancellationToken: cancellationToken, replyMarkup: new string[] { "Yes", "No" });
                }
                else if (!string.IsNullOrEmpty(update.Message.Text))
                {
                    if (user.Step == DocumentStep.WaitingForCarDocument)
                    {
                        switch(update.Message.Text)
                        {
                            case "Yes":
                                await _mediator.Send(new UpdateUserCommand(DocumentStep.WaitingForPassport, update.Message.From.Id));
                                //verify what is stored in cache and save extracted fields to db
                                await _botClient.SendMessage(chatId, "Car Document is valid! Now upload Passport.", cancellationToken: cancellationToken);
                                break;
                            case "No":
                                await _botClient.SendMessage(chatId, "Car Document is invalid! Reupload it.", cancellationToken: cancellationToken);
                                break;
                        }
                        
                    }
                    else if (user.Step == DocumentStep.WaitingForPassport )
                    {
                        switch (update.Message.Text)
                        {
                            case "Yes":
                                await _mediator.Send(new UpdateUserCommand(DocumentStep.Completed, update.Message.From.Id));
                                //verify what is stored in cache and save extracted fields to db
                                await _botClient.SendMessage(chatId, "Passport is valid! Please wait for car insurance proposal.", cancellationToken: cancellationToken);
                                break;
                            case "No":
                                await _botClient.SendMessage(chatId, "Passport is invalid! Reupload it.", cancellationToken: cancellationToken);
                                break;
                        }                        
                    }
                    else
                    {
                        reply = await _mediator.Send(new ProcessRequestByAIQuery(update.Message.Text), cancellationToken);
                    }
                }
                else
                {
                    reply = "Please send a document or text message.";
                }


            }
            catch (Exception ex)
            {
                await _botClient.SendMessage(chatId, "An error occurred while processing your request (Most likely OpenAI).", cancellationToken: cancellationToken);
            }

        }
    }
}
