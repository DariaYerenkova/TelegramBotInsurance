using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using InsurantSales.Application.OpenAIService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InsurantSales.Application.OpenAIService
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(IConfiguration config, IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _apiKey = config["OpenAI:ApiKey"]!;
        }

        public async Task<string> GetAIReplyAsync(string userInput)
        {
            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant for car insurant selling." },
                new { role = "user", content = userInput }
                },
                max_tokens = 500
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(result);
            return json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }
    }
}
