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
        
        public Task Consume(ConsumeContext<IPayPix> context) {
            var pix = context.Message;
            _logger.LogInformation($"Consuming pix with value: {pix.Value} and id: {pix.Id}");
            return Task.CompletedTask;
        }
    }
}