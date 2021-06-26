using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace SimpleWorker {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    
                    services.AddMassTransit(x => {
                        x.AddConsumer<MessageConsumer>();
                        x.UsingRabbitMq((context,cfg) => {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    
                    services.AddMassTransitHostedService(true);
                    services.AddHostedService<Worker>();
                });
    }
}