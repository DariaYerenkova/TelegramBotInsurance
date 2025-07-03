using InsurantSales.Application.DTOs;
using InsurantSales.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Mindee;
using Mindee.Input;
using Mindee.Product.Receipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InsurantSales.Application.MindeeService
{
    public class MindeeService : IMindeeService
    {
        private readonly MindeeClient _mindeeClient;

        public MindeeService(MindeeClient mindeeClient)
        {
            _mindeeClient = mindeeClient;
        }

        public async Task<List<ExtractedFieldDTO>> ExtractTextFromFileAsync(string filePaths, CancellationToken cancellationToken)
        {
            var inputSource = new LocalInputSource(filePaths);

            //var response = await _mindeeClient.ParseAsync<ReceiptV5>(inputSource);

            //var json = System.Text.Json.JsonSerializer.Serialize(response.Document);
            //fake mindee response
            var json = await File.ReadAllTextAsync("TestData/TestMindeeResponse.json", cancellationToken);

            var result = ParseMindeeResponse(json);
            return result;
        }

        private List<ExtractedFieldDTO> ParseMindeeResponse(string json)
        {
            var extractedFields = new List<ExtractedFieldDTO>();

            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("document", out var document) ||
                !document.TryGetProperty("inference", out var inference) ||
                !inference.TryGetProperty("prediction", out var prediction))
                return extractedFields;

            foreach (var property in prediction.EnumerateObject())
            {
                var fieldName = property.Name;
                var fieldObject = property.Value;

                string? value = null;
                double confidence = 0.0;

                if (fieldObject.ValueKind == JsonValueKind.Object)
                {
                    if (fieldObject.TryGetProperty("value", out var valueProp))
                        value = valueProp.ToString();

                    if (fieldObject.TryGetProperty("confidence", out var confProp) && confProp.ValueKind == JsonValueKind.Number)
                        confidence = confProp.GetDouble();
                }

                if (value != null)
                {
                    extractedFields.Add(new ExtractedFieldDTO(fieldName, value, confidence));
                }
            }

            return extractedFields;
        }


    }
}
