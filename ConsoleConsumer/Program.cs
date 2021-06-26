using System;
using System.Threading;
using System.Threading.Tasks;
using Events;
using MassTransit;

namespace ConsoleConsumer {
    class Program {
        public static async Task Main() {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => {
                cfg.ReceiveEndpoint("pix-listener", e => {
                    e.Consumer<PixConsumer>();
                });
            });
            
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try {
                Console.WriteLine("Press enter to exit");
                await Task.Run(() => Console.ReadLine());
            }
            finally { 
                await busControl.StopAsync();
            }
        }
    }
    
    class PixConsumer : IConsumer<IPayPix> {
        public async Task Consume(ConsumeContext<IPayPix> context) {
            // ReSharper disable once PossibleLossOfFraction
            Console.WriteLine($"Chegou o pix: {(decimal) (context.Message.Value / 100)} às {DateTimeOffset.Now}.");
            await Task.CompletedTask;
        }
    }
}