using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace PixConsumer.Consumers {
    public class PixPayedConsumer: IConsumer<IPixPayed> {
        
        private readonly ILogger<PixPayedConsumer> _logger;

        public PixPayedConsumer(ILogger<PixPayedConsumer> logger) {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPixPayed> context) {
            var pix = context.Message;
            _logger.LogInformation($"Sending email simulator for pix payed at: {pix.Time} | {pix.Pix.Id} ");
            var randomTime = new Random().Next(1000, 2000);
            await Task.Delay(randomTime);
            Console.WriteLine($"Done sending e-mail. | {pix.Pix.Id}");
        }
    }
}