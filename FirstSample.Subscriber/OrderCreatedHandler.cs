using System;
using System.Threading.Tasks;
using FirstSampe.Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace FirstSample.Subscriber
{
    public class OrderCreatedHandler : IHandleMessages<OrderPlaced>
    {
        private static readonly ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");
            return Task.Delay(0);
        }
    }
}
