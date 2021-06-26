using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace PixConsumer.Consumers {
    
    public class PayPixConsumer: IConsumer<IPayPix> {

        private readonly ILogger<PayPixConsumer> _logger;

        public PayPixConsumer(ILogger<PayPixConsumer> logger) {
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<IPayPix> context) {
            var pix = context.Message;
            _logger.LogInformation($"PayPix Value: {pix.Value} | id: {pix.Id}");

            await context.Publish<IPixPayed>(new {
                pix = pix,
                Time = DateTime.UtcNow,
            });
        }
    }
}