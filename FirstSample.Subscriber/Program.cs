using System;
using System.Reflection;
using System.Threading.Tasks;
using FirstSampe.Messages;
using NServiceBus;

namespace FirstSample.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "FirstSample.Subscriber";
            var endpointConfiguration = new EndpointConfiguration("FirstSample.Subscriber");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");

            //替代App.config的<MessageEndpointMappings/>配置
            //endpointConfiguration.UseTransport<MsmqTransport>()
            //    .Routing()
            //    .RouteToEndpoint(typeof(OrderPlaced), "FirstSample.Server");
            endpointConfiguration.UseTransport<MsmqTransport>()
                .Routing()
                .RegisterPublisher(typeof(OrderPlaced), "FirstSample.Server");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            try
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop().ConfigureAwait(false);
            }
        }
    }
}
