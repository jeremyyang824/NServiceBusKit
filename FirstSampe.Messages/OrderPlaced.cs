using System;
using NServiceBus;

namespace FirstSampe.Messages
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }
}
