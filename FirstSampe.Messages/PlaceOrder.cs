using System;
using NServiceBus;

namespace FirstSampe.Messages
{
    public class PlaceOrder : ICommand
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
    }
}
