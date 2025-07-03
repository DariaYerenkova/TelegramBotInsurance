using InsurantSales.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsurantSales.Application.OpenAIService.Interfaces;

namespace InsurantSales.Application.Handlers
{
    public class ProcessRequestByAIHandler : IRequestHandler<ProcessRequestByAIQuery, string>
    {
        private readonly IOpenAIService _ai;

        public ProcessRequestByAIHandler(IOpenAIService ai)
        {
            _ai = ai;
        }

        public async Task<string> Handle(ProcessRequestByAIQuery request, CancellationToken cancellationToken)
        {
            return await _ai.GetAIReplyAsync(request.userInput);
        }
    }
}
