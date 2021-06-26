using System;
using System.Threading;
using System.Threading.Tasks;
using Events;
using MassTransit;

namespace ConsolePublisher {
    class Program {
        public static async Task Main() {
            var busControl = Bus.Factory.CreateUsingRabbitMq();
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);

            try {
                while (true) {
                    string message = await Task.Run(() => {
                        Console.WriteLine("Digite o valor do Pix (XX.XX) | (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });
                    
                    if("quit".Equals(message, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<IPayPix>(new {
                        Value = Convert.ToUInt64(message),
                    });
                }
            }
            finally { 
                await busControl.StopAsync();
            }
        }
    }
}