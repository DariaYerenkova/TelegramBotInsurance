using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsurantSales.Application.Queries;
using InsurantSales.Application.MindeeService;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Xml.Linq;
using InsurantSales.Application.Commands;
using InsurantSales.Application.Interfaces;
using InsurantSales.Domain.Entities;
using InsurantSales.Application.DTOs;

namespace InsurantSales.Application.Handlers
{
    public class ProcessUploadedDocumentHandler : IRequestHandler<ProcessUploadedDocumentQuery, string>, IRequestHandler<SaveDocumentToLocalFolderCommand, string>, IRequestHandler<SaveDocumentToDBCommand, Guid>
    {
        private readonly ITelegramBotClient _bot;
        private readonly IMindeeService _mindee;
        private readonly IDocumentService _documentService;

        public ProcessUploadedDocumentHandler(ITelegramBotClient bot, IMindeeService mindee, IDocumentService documentService)
        {
            _bot = bot;
            _mindee = mindee;
            _documentService = documentService;
        }

        public async Task<string> Handle(ProcessUploadedDocumentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mindee.ExtractTextFromFileAsync(request.filePaths, cancellationToken);
                //convert dto to ExtractedField and save to cache
                var convertedToStringResult = FormatExtractedFields(result);
                return $"Confirm extracted info:\n\n{convertedToStringResult}";
            }
            catch
            {
                return "Failed to process document.";
            }
        }

        public async Task<string> Handle(SaveDocumentToLocalFolderCommand request, CancellationToken cancellationToken)
        {
            return await _documentService.SaveDocumentToLocalFolderAsync(request.fileStream, request.fileName, request.telegramUserId);
        }

        public async Task<Guid> Handle(SaveDocumentToDBCommand request, CancellationToken cancellationToken)
        {
            return await _documentService.SaveDocumentToDBAsync(request.document);
        }

        private static string FormatExtractedFields(List<ExtractedFieldDTO> fields)
        {
            var sb = new StringBuilder();

            foreach (var field in fields)
            {
                sb.AppendLine($"• {field.FieldName}: {field.Value} ({field.Confidence}%)");
            }

            return sb.ToString();
        }

    }
}
