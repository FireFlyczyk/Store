using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Dtos;

namespace OrderService.MessageBus
{
    public interface IMessageBus
    {
        void PublishNewOrder(OrderSendDto orderSendDto);
    }
}