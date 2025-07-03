using InsurantSales.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InsurantSales.Application.Handlers
{
    public class ProcessStartHandler : IRequestHandler<ProcessStartQuery, string>
    {
        public async Task<string> Handle(ProcessStartQuery request, CancellationToken cancellationToken)
        {
            var message = "Welcome to Insurant sales Bot!\n\n" +
                          "I can help you get a car insurance quote.\n\n" +
                          "Please answer questions or ask anything.\n" +
                          "If you want to get an insurance proposal for your car, please upload your car document.";

            return message;
        }
    }
}
